using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

using CluedIn.Core;
using CluedIn.Core.Configuration;
using CluedIn.Core.Data;
using CluedIn.Core.Data.Parts;
using CluedIn.Crawling.Helpers;
using CluedIn.ExternalSearch.Filters;
using CluedIn.ExternalSearch.Providers.Edgar.Models;
using CluedIn.ExternalSearch.Providers.Edgar.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Edgar
{
    /// <summary>The edgar graph external search provider.</summary>
    /// <seealso cref="CluedIn.ExternalSearch.ExternalSearchProviderBase" />
    public class EdgarExternalSearchProvider : ExternalSearchProviderBase
    {
        public static readonly Guid ProviderId = Guid.Parse("be6a7f61-fe14-4b47-9d56-c49138a329ae");   // TODO: Replace value

        /**********************************************************************************************************
         * CONSTRUCTORS
         **********************************************************************************************************/

        public EdgarExternalSearchProvider()
            : base(ProviderId, EntityType.Organization)
        {
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Builds the queries.</summary>
        /// <param name="context">The context.</param>
        /// <param name="request">The request.</param>
        /// <returns>The search queries.</returns>
        public override IEnumerable<IExternalSearchQuery> BuildQueries(ExecutionContext context, IExternalSearchRequest request)
        {
            if (!this.Accepts(request.EntityMetaData.EntityType))
                yield break;

            var existingResults = request.GetQueryResults<EdgarResponse>(this).ToList();

            Func<string, bool> nameFilter = value => OrganizationFilters.NameFilter(context, value) || existingResults.Any(r => string.Equals(r.Data.CompanyInfo.ConformedName, value, StringComparison.InvariantCultureIgnoreCase));

            // Query Input
            //For companies use CluedInOrganization vocab, for people use CluedInPerson and so on for different types.
            var entityType = request.EntityMetaData.EntityType;
            var organizationName = request.QueryParameters.GetValue(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName, new HashSet<string>());

            if (!string.IsNullOrEmpty(request.EntityMetaData.Name))
                organizationName.Add(request.EntityMetaData.Name);
            if (!string.IsNullOrEmpty(request.EntityMetaData.DisplayName))
                organizationName.Add(request.EntityMetaData.DisplayName);

            if (organizationName != null)
            {
                var values = organizationName.Select(NameNormalization.Normalize).ToHashSet();

                foreach (var value in values.Where(v => !nameFilter(v)))
                    yield return new ExternalSearchQuery(this, entityType, ExternalSearchQueryParameter.Name, value);
            }
        }

        /// <summary>Executes the search.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <returns>The results.</returns>
        public override IEnumerable<IExternalSearchQueryResult> ExecuteSearch(ExecutionContext context, IExternalSearchQuery query)
        {
            var name = query.QueryParameters[ExternalSearchQueryParameter.Name].FirstOrDefault();

            if (string.IsNullOrEmpty(name))
                yield break;

            EdgarResponse response = null;
            try
            {
                var serializer = new XmlSerializer(typeof(EdgarResponse));

                var reader = XmlReader.Create(string.Format("https://www.sec.gov/cgi-bin/browse-edgar?company={0}&owner=exclude&action=getcompany&count=10&output=atom", name));

                response = (EdgarResponse)serializer.Deserialize(reader);

                reader.Close();
            }

            catch (Exception exception)
            {
                throw new Exception("Could not execute EDGAR external search query", exception);
            }

            if (response != null)
            {
                if (response.CompanyInfo != null)
                    yield return new ExternalSearchQueryResult<CompanyInfo>(query, response.CompanyInfo);

                if (response.Entry == null)
                    yield break;

                //foreach (var value in response.Entry)
                //{
                //    if (value != null)
                //    {
                //        if (value.Content?.CompanyInfo != null)
                //            yield return new ExternalSearchQueryResult<CompanyInfo>(query, value.Content.CompanyInfo);
                //        else
                //            value.Content.CompanyInfo = response.CompanyInfo;
                //        yield return new ExternalSearchQueryResult<Entry>(query, value);
                //    }
                //}
            }
        }

        /// <summary>Builds the clues.</summary>
        /// <param name="context">The context.</param>
        /// <param name="query">The query.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The clues.</returns>
        public override IEnumerable<Clue> BuildClues(ExecutionContext context, IExternalSearchQuery query, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItem = result.As<CompanyInfo>();

            var code = this.GetOriginEntityCode(resultItem);

            var clue = new Clue(code, context.Organization);

            this.PopulateMetadata(clue.Data.EntityData, resultItem);

            yield return clue;
        }

        /// <summary>Gets the primary entity metadata.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The primary entity metadata.</returns>
        public override IEntityMetadata GetPrimaryEntityMetadata(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            var resultItemCompany = result.As<CompanyInfo>();

            if (resultItemCompany == null)
            {
                var resultItem = result.As<Entry>();

                return this.CreateMetadata(resultItem);
            }

            return this.CreateMetadata(resultItemCompany.As<CompanyInfo>());
        }

        /// <summary>Gets the preview image.</summary>
        /// <param name="context">The context.</param>
        /// <param name="result">The result.</param>
        /// <param name="request">The request.</param>
        /// <returns>The preview image.</returns>
        public override IPreviewImage GetPrimaryEntityPreviewImage(ExecutionContext context, IExternalSearchQueryResult result, IExternalSearchRequest request)
        {
            return null;
        }

        /// <summary>Creates the metadata.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The metadata.</returns>
        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<CompanyInfo> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        private IEntityMetadata CreateMetadata(IExternalSearchQueryResult<Entry> resultItem)
        {
            var metadata = new EntityMetadataPart();

            this.PopulateMetadata(metadata, resultItem);

            return metadata;
        }

        /// <summary>Gets the origin entity code.</summary>
        /// <param name="resultItem">The result item.</param>
        /// <returns>The origin entity code.</returns>
        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<CompanyInfo> resultItem)
        {
            return new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Data.Cik);
        }

        private EntityCode GetOriginEntityCode(IExternalSearchQueryResult<Entry> resultItem)
        {
            return new EntityCode(EntityType.Files.File, this.GetCodeOrigin(), resultItem.Data.Id);
        }

        /// <summary>Gets the code origin.</summary>
        /// <returns>The code origin</returns>
        private CodeOrigin GetCodeOrigin()
        {
            return CodeOrigin.CluedIn.CreateSpecific("edgar");
        }

        /// <summary>Populates the metadata.</summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="resultItem">The result item.</param>
        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<CompanyInfo> resultItem)
        {
            var code = this.GetOriginEntityCode(resultItem);

            metadata.EntityType = EntityType.Organization;

            metadata.Name = resultItem.Data.ConformedName;
            metadata.Description = resultItem.Data.Assignedsicdesc;
            metadata.OriginEntityCode = code;

            metadata.Codes.Add(code);
            metadata.Codes.Add(new EntityCode(EntityType.Organization, this.GetCodeOrigin(), resultItem.Data.Cik));

            if (resultItem.Data.Author != null)
                if (!string.IsNullOrEmpty(resultItem.Data.Author.Email))
                    metadata.Authors.Add(new PersonReference(resultItem.Data.Author.Name, new EntityCode(EntityType.Infrastructure.User, GetCodeOrigin(), resultItem.Data.Author.Email)));

            if (!string.IsNullOrEmpty(resultItem.Data.Cikhref))
                metadata.Uri = new Uri(resultItem.Data.Cikhref);

            metadata.ModifiedDate = resultItem.Data.Lastdate.ParseAsDateTimeOffset();

            metadata.Properties[EdgarVocabulary.Organization.AssistantDirector] = resultItem.Data.Assitantdirector;
            metadata.Properties[EdgarVocabulary.Organization.Sic] = resultItem.Data.Assignedsic;
            metadata.Properties[EdgarVocabulary.Organization.Cik] = resultItem.Data.Cik;
            metadata.Properties[EdgarVocabulary.Organization.FiscalYearEnd] = resultItem.Data.Fiscalyearend;
            metadata.Properties[EdgarVocabulary.Organization.Name] = resultItem.Data.ConformedName;

            if (resultItem.Data.Addresses?.Address != null)
            {
                if (resultItem.Data.Addresses.Address.Find(c => c.Type == "business") != null)
                    PopulateAddress(metadata, EdgarVocabulary.Organization.BusinessAddress, resultItem.Data.Addresses.Address.Find(c => c.Type == "business"));

                if (resultItem.Data.Addresses.Address.Find(c => c.Type == "mailing") != null)
                    PopulateAddress(metadata, EdgarVocabulary.Organization.MailingAddress, resultItem.Data.Addresses.Address.Find(c => c.Type == "mailing"));
            }

            if (resultItem.Data.Formerlynames == null)
                return;
            if (resultItem.Data.Formerlynames.Names == null)
                return;

            resultItem.Data.Formerlynames.Names.OrderByDescending(value => value.Date);
            for (int i = 0; i < resultItem.Data.Formerlynames.Names.Count; i++)
            {
                metadata.Properties[string.Format("edgar.organization.formelyName{0}", i + 1)] = resultItem.Data.Formerlynames.Names[i].Name;
                metadata.Properties[string.Format("edgar.organization.formelyName{0}Date", i + 1)] = resultItem.Data.Formerlynames.Names[i].Date;
            }
        }

        private static void PopulateAddress(IEntityMetadata metadata, EdgarAddressVocabulary vocabulary, Address address)
        {
            metadata.Properties[vocabulary.City] = address.City.PrintIfAvailable();
            metadata.Properties[vocabulary.Phone] = address.Phone.PrintIfAvailable();
            metadata.Properties[vocabulary.State] = address.State.PrintIfAvailable();
            metadata.Properties[vocabulary.Street1] = address.Street1.PrintIfAvailable();
            metadata.Properties[vocabulary.Street2] = address.Street2.PrintIfAvailable();
            metadata.Properties[vocabulary.Zip] = address.Zip.PrintIfAvailable();
        }

        private void PopulateMetadata(IEntityMetadata metadata, IExternalSearchQueryResult<Entry> resultItem)
        {
            var code = new EntityCode(EntityType.Files.File, this.GetCodeOrigin(), resultItem.Data.Id);

            metadata.Codes.Add(code);
            metadata.EntityType = EntityType.Files.File;

            if (resultItem.Data == null)
                return;

            metadata.Name = resultItem.Data.Title;

            if (resultItem.Data.Author != null)
                if (!string.IsNullOrEmpty(resultItem.Data.Author.Email))
                    metadata.Authors.Add(new PersonReference(resultItem.Data.Author.Name, new EntityCode(EntityType.Infrastructure.User, GetCodeOrigin(), resultItem.Data.Author.Email)));

            if (resultItem.Data.Summary != null)
                if (!string.IsNullOrEmpty(resultItem.Data.Summary.Text))
                    metadata.Description = Regex.Replace(resultItem.Data.Summary.Text, "<.*?>", String.Empty);

            if (resultItem.Data.Link != null)
                if (!string.IsNullOrEmpty(resultItem.Data.Link.Href))
                    metadata.Uri = new Uri(resultItem.Data.Link.Href);

            metadata.ModifiedDate = resultItem.Data.Updated.ParseAsDateTimeOffset();
            metadata.CreatedDate = resultItem.Data.Content.Filingdate.ParseAsDateTimeOffset();

            if (resultItem.Data.Category != null)
                metadata.Properties[EdgarVocabulary.Entry.Category] = resultItem.Data.Category.Term.PrintIfAvailable();

            if (resultItem.Data.Content == null)
                return;

            if (!string.IsNullOrEmpty(resultItem.Data.Content.Accessionnunber))
                metadata.Codes.Add(new EntityCode(EntityType.Files.File, this.GetCodeOrigin(), resultItem.Data.Content.Accessionnunber));

            metadata.Properties[EdgarVocabulary.Entry.Size] = resultItem.Data.Content.Size.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Act] = resultItem.Data.Content.Act.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Amend] = resultItem.Data.Content.Amend.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Filenumber] = resultItem.Data.Content.Filenumber.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Filingdate] = resultItem.Data.Content.Filingdate.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Filingtype] = resultItem.Data.Content.Filingtype.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Filmnumber] = resultItem.Data.Content.Filmnumber.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Formname] = resultItem.Data.Content.Formname.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Itemsdesc] = resultItem.Data.Content.Itemsdesc.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Paper_filing] = resultItem.Data.Content.Paper_filing.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Type] = resultItem.Data.Content.Type.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Xbrl_href] = resultItem.Data.Content.Xbrl_href.PrintIfAvailable();
            metadata.Properties[EdgarVocabulary.Entry.Filenumberhref] = resultItem.Data.Content.Filenumberhref.PrintIfAvailable();

            //TODO: Download and index text file, set webpage as preview image
            //var uri = new UriBuilder(resultItem.Data.Content.Filinghref);

            //var txt = uri;
            //txt.Uri.Segments[uri.Uri.Segments.Count()-1] = string.Format("{0}.txt", resultItem.Data.Content.Accessionnunber);

            //var page = uri;

            //var name = string.Empty; //TODO: Deserialize text file and get FILENAME to construct uri below
            //txt.Uri.Segments[uri.Uri.Segments.Count() - 1] = string.Format("{0}.txt", name);

            if (resultItem.Data.Content.CompanyInfo != null)
                if (!string.IsNullOrEmpty(resultItem.Data.Content.CompanyInfo.Cik))
                    metadata.OutgoingEdges.Add(new EntityEdge(new EntityReference(code), new EntityReference(EntityType.Organization, resultItem.Data.Content.CompanyInfo.Cik), EntityEdgeType.CreatedBy));
        }
    }
}
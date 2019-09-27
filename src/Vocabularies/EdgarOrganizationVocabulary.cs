using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Edgar.Vocabularies
{
    public class EdgarOrganizationVocabulary : SimpleVocabulary
    {        
        public EdgarOrganizationVocabulary()
        {
            this.VocabularyName = "EDGAR Organization";
            this.KeyPrefix      = "edgar.organization";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Organization;

            AddGroup("Metadata", group =>
            {
                Cik                    = group.Add(new VocabularyKey("CIK", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Visible));
                Name                   = group.Add(new VocabularyKey("name", VocabularyKeyDataType.OrganizationName, VocabularyKeyVisibility.Visible));
                FiscalYearEnd          = group.Add(new VocabularyKey("fiscalYearEnd", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Visible));
                AssistantDirector      = group.Add(new VocabularyKey("assistantDirector", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Sic                    = group.Add(new VocabularyKey("SIC", VocabularyKeyDataType.Integer, VocabularyKeyVisibility.Visible));
            });

            this.AddGroup("Location", group =>
            {
                this.BusinessAddress = group.Add(new EdgarAddressVocabulary().AsCompositeKey("businessAddress"));
                this.MailingAddress = group.Add(new EdgarAddressVocabulary().AsCompositeKey("mailingAddress", VocabularyKeyVisibility.Hidden));
            });

            this.AddMapping(this.Cik, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesCIK);
            this.AddMapping(this.Sic, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.CodesSIC);
            this.AddMapping(this.Name, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.OrganizationName);
        }
     
        public VocabularyKey Cik { get; private set; }
        public VocabularyKey Name { get; private set; }
        public VocabularyKey FiscalYearEnd { get; private set; }
        public VocabularyKey AssistantDirector { get; private set; }
        public VocabularyKey Sic { get; private set; }
        public EdgarAddressVocabulary BusinessAddress { get; private set; }
        public EdgarAddressVocabulary MailingAddress { get; private set; }

    }
}

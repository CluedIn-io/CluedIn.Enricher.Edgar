using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Edgar.Vocabularies
{
    public class EdgarAddressVocabulary : SimpleVocabulary
    {
        public EdgarAddressVocabulary()
        {
            this.VocabularyName = "EDGAR Address";
            this.KeyPrefix      = "edgar.address";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Geography;

            Street1 = this.Add(new VocabularyKey("street1", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            Street2 = this.Add(new VocabularyKey("street2", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            Zip     = this.Add(new VocabularyKey("zip", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            City    = this.Add(new VocabularyKey("city", VocabularyKeyDataType.GeographyCity, VocabularyKeyVisibility.Visible));
            State   = this.Add(new VocabularyKey("state", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
            Phone   = this.Add(new VocabularyKey("phone", VocabularyKeyDataType.PhoneNumber, VocabularyKeyVisibility.Visible));

            this.AddMapping(this.City, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressCity);
            this.AddMapping(this.Phone, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.PhoneNumber);
            this.AddMapping(this.State, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressState);
            this.AddMapping(this.Street1, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Address);
            this.AddMapping(this.Street2, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.AddressZipCode);
        }

        public VocabularyKey City { get; private set; }
        public VocabularyKey Phone { get; private set; }
        public VocabularyKey State { get; private set; }
        public VocabularyKey Street1 { get; private set; }
        public VocabularyKey Street2 { get; private set; }
        public VocabularyKey Zip { get; private set; }
    }
}

using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Edgar.Vocabularies
{
    public static class EdgarVocabulary
    {
        static EdgarVocabulary()
        {
            Organization = new EdgarOrganizationVocabulary();
            Address = new EdgarAddressVocabulary();
            Entry = new EdgarEntryVocabulary();
        }

        public static EdgarOrganizationVocabulary Organization { get; private set; }
        public static EdgarAddressVocabulary Address { get; private set; }
        public static EdgarEntryVocabulary Entry { get; private set; }
    }
}

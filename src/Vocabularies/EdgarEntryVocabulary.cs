using CluedIn.Core.Data;
using CluedIn.Core.Data.Vocabularies;

namespace CluedIn.ExternalSearch.Providers.Edgar.Vocabularies
{
    public class EdgarEntryVocabulary : SimpleVocabulary
    {
        public EdgarEntryVocabulary()
        {
            this.VocabularyName = "EDGAR Entry";
            this.KeyPrefix      = "edgar.entry";
            this.KeySeparator   = ".";
            this.Grouping       = EntityType.Files.File;

            AddGroup("Metadata", group =>
            {
                Category       = group.Add(new VocabularyKey("category", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Size           = group.Add(new VocabularyKey("size", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Act            = group.Add(new VocabularyKey("act", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Amend          = group.Add(new VocabularyKey("amend", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Filenumber     = group.Add(new VocabularyKey("fileNumber", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Filingdate     = group.Add(new VocabularyKey("filingDate", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Filingtype     = group.Add(new VocabularyKey("filingType", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Formname       = group.Add(new VocabularyKey("formName", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Filmnumber     = group.Add(new VocabularyKey("filmNumber", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Itemsdesc      = group.Add(new VocabularyKey("itemsDesc", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Paper_filing   = group.Add(new VocabularyKey("paperFiling", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Type           = group.Add(new VocabularyKey("type", VocabularyKeyDataType.Text, VocabularyKeyVisibility.Visible));
                Xbrl_href      = group.Add(new VocabularyKey("xbrlHref", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
                Filenumberhref = group.Add(new VocabularyKey("fileNumberHref", VocabularyKeyDataType.Uri, VocabularyKeyVisibility.Visible));
            });

            this.AddMapping(this.Category, CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInFile.Classification);
        }

        public VocabularyKey Category { get; private set; }
        public VocabularyKey Size { get; private set; }
        public VocabularyKey Act { get; private set; }
        public VocabularyKey Amend { get; private set; }
        public VocabularyKey Filenumber { get; private set; }
        public VocabularyKey Filingdate { get; private set; }
        public VocabularyKey Filingtype { get; private set; }
        public VocabularyKey Filmnumber { get; private set; }
        public VocabularyKey Formname { get; private set; }
        public VocabularyKey Itemsdesc { get; private set; }
        public VocabularyKey Paper_filing { get; private set; }
        public VocabularyKey Type { get; private set; }
        public VocabularyKey Xbrl_href { get; private set; }
        public VocabularyKey Filenumberhref { get; private set; }
    }
}

using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
    public class Content
    {
        [XmlElement(ElementName = "accession-nunber", Namespace = "http://www.w3.org/2005/Atom")]
        public string Accessionnunber { get; set; }
        [XmlElement(ElementName = "act", Namespace = "http://www.w3.org/2005/Atom")]
        public string Act { get; set; }
        [XmlElement(ElementName = "file-number", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filenumber { get; set; }
        [XmlElement(ElementName = "file-number-href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filenumberhref { get; set; }
        [XmlElement(ElementName = "filing-date", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filingdate { get; set; }
        [XmlElement(ElementName = "filing-href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filinghref { get; set; }
        [XmlElement(ElementName = "filing-type", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filingtype { get; set; }
        [XmlElement(ElementName = "film-number", Namespace = "http://www.w3.org/2005/Atom")]
        public string Filmnumber { get; set; }
        [XmlElement(ElementName = "form-name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Formname { get; set; }
        [XmlElement(ElementName = "size", Namespace = "http://www.w3.org/2005/Atom")]
        public string Size { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "xbrl_href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Xbrl_href { get; set; }
        [XmlElement(ElementName = "items-desc", Namespace = "http://www.w3.org/2005/Atom")]
        public string Itemsdesc { get; set; }
        [XmlElement(ElementName = "amend", Namespace = "http://www.w3.org/2005/Atom")]
        public string Amend { get; set; }
        [XmlElement(ElementName = "paper_filing", Namespace = "http://www.w3.org/2005/Atom")]
        public string Paper_filing { get; set; }

        [XmlElement(ElementName = "company-info", Namespace = "http://www.w3.org/2005/Atom")]
        public CompanyInfo CompanyInfo { get; set; }
    }
}
   

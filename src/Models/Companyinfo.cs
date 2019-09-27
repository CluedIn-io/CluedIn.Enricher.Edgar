using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "company-info", Namespace = "http://www.w3.org/2005/Atom")]
    public class CompanyInfo
    {
        [XmlElement(ElementName = "addresses", Namespace = "http://www.w3.org/2005/Atom")]
        public Addresses Addresses { get; set; }
        [XmlElement(ElementName = "assigned-sic", Namespace = "http://www.w3.org/2005/Atom")]
        public string Assignedsic { get; set; }
        [XmlElement(ElementName = "assigned-sic-desc", Namespace = "http://www.w3.org/2005/Atom")]
        public string Assignedsicdesc { get; set; }
        [XmlElement(ElementName = "assigned-sic-href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Assignedsichref { get; set; }
        [XmlElement(ElementName = "assitant-director", Namespace = "http://www.w3.org/2005/Atom")]
        public string Assitantdirector { get; set; }
        [XmlElement(ElementName = "cik", Namespace = "http://www.w3.org/2005/Atom")]
        public string Cik { get; set; }
        [XmlElement(ElementName = "cik-href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Cikhref { get; set; }
        [XmlElement(ElementName = "conformed-name", Namespace = "http://www.w3.org/2005/Atom")]
        public string ConformedName { get; set; }
        [XmlElement(ElementName = "fiscal-year-end", Namespace = "http://www.w3.org/2005/Atom")]
        public string Fiscalyearend { get; set; }
        [XmlElement(ElementName = "state-location", Namespace = "http://www.w3.org/2005/Atom")]
        public string Statelocation { get; set; }
        [XmlElement(ElementName = "state-location-href", Namespace = "http://www.w3.org/2005/Atom")]
        public string Statelocationhref { get; set; }
        [XmlElement(ElementName = "state-of-incorporation", Namespace = "http://www.w3.org/2005/Atom")]
        public string Stateofincorporation { get; set; }

        [XmlElement(ElementName = "last-date", Namespace = "http://www.w3.org/2005/Atom")]
        public string Lastdate { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; }
        [XmlElement(ElementName = "revoke-flag", Namespace = "http://www.w3.org/2005/Atom")]
        public string Revokeflag { get; set; }
        [XmlElement(ElementName = "irs-number", Namespace = "http://www.w3.org/2005/Atom")]
        public string Irsnumber { get; set; }
        [XmlElement(ElementName = "state", Namespace = "http://www.w3.org/2005/Atom")]
        public string State { get; set; }
        [XmlElement(ElementName = "formerly-names", Namespace = "http://www.w3.org/2005/Atom")]
        public Formerlynames Formerlynames { get; set; }

        public string Link { get; set; }
        public Author Author { get; set; }
    }
    [XmlRoot(ElementName = "names", Namespace = "http://www.w3.org/2005/Atom")]
    public class Names
    {
        [XmlElement(ElementName = "date", Namespace = "http://www.w3.org/2005/Atom")]
        public string Date { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "formerly-names", Namespace = "http://www.w3.org/2005/Atom")]
    public class Formerlynames
    {
        [XmlElement(ElementName = "names", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Names> Names { get; set; }
        [XmlAttribute(AttributeName = "count")]
        public string Count { get; set; }
    }

}
   

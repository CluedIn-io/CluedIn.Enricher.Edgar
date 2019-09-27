using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "feed", Namespace = "http://www.w3.org/2005/Atom")]
    public class EdgarResponse
    {
        [XmlElement(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
        public Author Author { get; set; }
        [XmlElement(ElementName = "company-info", Namespace = "http://www.w3.org/2005/Atom")]
        public CompanyInfo CompanyInfo { get; set; }
        [XmlElement(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Entry> Entry { get; set; }
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Link> Link { get; set; }
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; }
        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }
        [XmlAttribute(AttributeName = "xmlns")]
        public string Xmlns { get; set; }
    }
}
   

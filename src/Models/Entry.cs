using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "entry", Namespace = "http://www.w3.org/2005/Atom")]
    public class Entry
    {
        [XmlElement(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
        public Category Category { get; set; }
        [XmlElement(ElementName = "content", Namespace = "http://www.w3.org/2005/Atom")]
        public Content Content { get; set; }
        [XmlElement(ElementName = "id", Namespace = "http://www.w3.org/2005/Atom")]
        public string Id { get; set; }
        [XmlElement(ElementName = "link", Namespace = "http://www.w3.org/2005/Atom")]
        public Link Link { get; set; }
        [XmlElement(ElementName = "summary", Namespace = "http://www.w3.org/2005/Atom")]
        public Summary Summary { get; set; }
        [XmlElement(ElementName = "title", Namespace = "http://www.w3.org/2005/Atom")]
        public string Title { get; set; }
        [XmlElement(ElementName = "updated", Namespace = "http://www.w3.org/2005/Atom")]
        public string Updated { get; set; }

        public Author Author { get; set; }
    }
}
   

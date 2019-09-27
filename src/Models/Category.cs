using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "category", Namespace = "http://www.w3.org/2005/Atom")]
    public class Category
    {
        [XmlAttribute(AttributeName = "label")]
        public string Label { get; set; }
        [XmlAttribute(AttributeName = "scheme")]
        public string Scheme { get; set; }
        [XmlAttribute(AttributeName = "term")]
        public string Term { get; set; }
    }
}
   

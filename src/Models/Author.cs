using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "author", Namespace = "http://www.w3.org/2005/Atom")]
    public class Author
    {
        [XmlElement(ElementName = "email", Namespace = "http://www.w3.org/2005/Atom")]
        public string Email { get; set; }
        [XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
        public string Name { get; set; }
    }
}
   

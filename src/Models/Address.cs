using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "address", Namespace = "http://www.w3.org/2005/Atom")]
    public class Address
    {
        [XmlElement(ElementName = "city", Namespace = "http://www.w3.org/2005/Atom")]
        public string City { get; set; }
        [XmlElement(ElementName = "state", Namespace = "http://www.w3.org/2005/Atom")]
        public string State { get; set; }
        [XmlElement(ElementName = "street1", Namespace = "http://www.w3.org/2005/Atom")]
        public string Street1 { get; set; }
        [XmlElement(ElementName = "street2", Namespace = "http://www.w3.org/2005/Atom")]
        public string Street2 { get; set; }
        [XmlElement(ElementName = "zip", Namespace = "http://www.w3.org/2005/Atom")]
        public string Zip { get; set; }
        [XmlAttribute(AttributeName = "type")]
        public string Type { get; set; }
        [XmlElement(ElementName = "phone", Namespace = "http://www.w3.org/2005/Atom")]
        public string Phone { get; set; }
    }
}
   

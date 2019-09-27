using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
    [XmlRoot(ElementName = "addresses", Namespace = "http://www.w3.org/2005/Atom")]
    public class Addresses
    {
        [XmlElement(ElementName = "address", Namespace = "http://www.w3.org/2005/Atom")]
        public List<Address> Address { get; set; }
    }
}
   

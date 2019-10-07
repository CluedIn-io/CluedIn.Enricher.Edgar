using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
	[XmlRoot(ElementName = "names", Namespace = "http://www.w3.org/2005/Atom")]
	public class Names
	{
		[XmlElement(ElementName = "date", Namespace = "http://www.w3.org/2005/Atom")]
		public string Date { get; set; }
		[XmlElement(ElementName = "name", Namespace = "http://www.w3.org/2005/Atom")]
		public string Name { get; set; }
	}
}
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CluedIn.ExternalSearch.Providers.Edgar.Models
{
	[XmlRoot(ElementName = "formerly-names", Namespace = "http://www.w3.org/2005/Atom")]
	public class Formerlynames
	{
		[XmlElement(ElementName = "names", Namespace = "http://www.w3.org/2005/Atom")]
		public List<Names> Names { get; set; }
		[XmlAttribute(AttributeName = "count")]
		public string Count { get; set; }
	}
}
using System.Xml.Serialization;

namespace ShortestWay.Model
{
    [XmlRoot("link")]
    public class Link
    {
        [XmlAttribute("ref")]
        public virtual int Ref { get; set; }

        [XmlAttribute("weight")]
        public virtual int Weight { get; set; }
    }
}
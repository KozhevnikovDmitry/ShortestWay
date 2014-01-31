using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace ShortestWay.Data
{
    [XmlRoot("graph")]
    public class Graph
    {
        [XmlElement("node")]
        public virtual Node[] Nodes { get; set; }

        public static Graph Create(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(Graph));
            using (var xr = new XmlTextReader(path))
            {
                var result = (Graph)xmlSerializer.Deserialize(xr);
                foreach (var node in result.Nodes)
                {
                    node.Graph = result;
                }
                return result;
            }
        }
    }

    [XmlRoot("node")]
    public class Node
    {
        [XmlAttribute("id")]
        public virtual int Id { get; set; }

        [XmlAttribute("role")]
        public virtual string Role { get; set; }

        [XmlAttribute("status")]
        public virtual string Status { get; set; }

        [XmlElement("link")]
        public virtual Link[] Links { get; set; }

        public virtual bool IsStart
        {
            get { return Role == "start"; }
        }

        public virtual bool IsFinish
        {
            get { return Role == "finish"; }
        }

        public virtual bool IsCrash
        {
            get { return Status == "crash"; }
        }

        public virtual bool IsLinked(Node node)
        {
            if (IsCrash)
            {
                return false;
            }

            return Links.Any(t => t.Ref == node.Id);
        }

        public virtual List<Node> Linked()
        {
            return Graph.Nodes.Where(t => t.IsLinked(this)).ToList();
        }

        public virtual Graph Graph { get; set; }

        public virtual int? Mark { get; set; }
    }

    [XmlRoot("link")]
    public class Link
    {
        [XmlAttribute("ref")]
        public virtual int Ref { get; set; }

        [XmlAttribute("weight")]
        public virtual int Weight { get; set; }
    }
}

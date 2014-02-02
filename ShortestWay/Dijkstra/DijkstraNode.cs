using System.Xml.Serialization;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    [XmlRoot("node")]
    public class DijkstraNode : Node
    {
        public DijkstraNode()
        {
            Mark = null;
            IsVisited = false;
            Previous = null;
        }

        public virtual int? Mark { get; set; }

        public virtual DijkstraNode Previous { get; set; }

        public virtual bool IsVisited { get; set; }
    }
}
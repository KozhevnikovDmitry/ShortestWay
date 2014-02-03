using System.Xml.Serialization;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    /// <summary>
    /// Road node with functionality for a Dijkstra algorythm. <see cref="Dijkstra"/>
    /// </summary>
    [XmlRoot("node")]
    public class DijkstraNode : Node
    {
        public DijkstraNode()
        {
            TotalWeigth = null;
            IsVisited = false;
            Previous = null;
        }

        /// <summary>
        /// Total weight of the shortest way from start node to current. Null means infinity
        /// </summary>
        public virtual int? TotalWeigth { get; set; }

        /// <summary>
        /// Previous node in shortest way from start node to current
        /// </summary>
        public virtual DijkstraNode Previous { get; set; }

        /// <summary>
        /// Returns true if node has been already visited
        /// </summary>
        public virtual bool IsVisited { get; set; }
    }
}
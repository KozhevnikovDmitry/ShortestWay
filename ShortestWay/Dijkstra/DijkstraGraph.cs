using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    /// <summary>
    /// Road graph with functionality for a Dijkstra algorythm. <see cref="Dijkstra"/>
    /// </summary>
    [XmlRoot("graph")]
    public class DijkstraGraph : Graph<DijkstraNode>
    {
        /// <summary>
        /// Setups initial dijkstra marks for graph nodes
        /// Mark = 0 for start node, null (infinity) for others
        /// </summary>
        public virtual void Markup()
        {
            foreach (var node in Nodes)
            {
                node.TotalWeigth = node.IsStart ? (int?)0 : null;
            }
        }

        /// <summary>
        /// Returns true if there are unvisited nodes in graph
        /// </summary>
        public virtual bool HasUnvisited()
        {
            return Unvisited().Any();
        }

        /// <summary>
        /// Returns first unvisited node with minimal mark
        /// </summary>
        /// <exception cref="GraphHasNotUnvisitedNodesException"></exception>
        public virtual DijkstraNode NearestUnvisited()
        {
            if (!HasUnvisited())
            {
                throw new GraphHasNotUnvisitedNodesException();
            }

            var minimalMark = Unvisited().Select(t => t.TotalWeigth).Min();
            return Unvisited().First(t => t.TotalWeigth == minimalMark);
        }

        /// <summary>
        /// Returns list of unvisited nodes
        /// </summary>
        private IList<DijkstraNode> Unvisited()
        {
            return Nodes.Where(t => t.IsVisited == false).Where(t => t.TotalWeigth.HasValue).ToList();
        }
    }
}
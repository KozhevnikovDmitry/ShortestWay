using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    [XmlRoot("graph")]
    public class DijkstraGraph : Graph<DijkstraNode>
    {
        public virtual void Markup()
        {
            foreach (var node in Nodes)
            {
                node.Mark = node.IsStart ? (int?)0 : null;
            }
        }

        public virtual bool HasUnvisited()
        {
            return Unvisited().Any();
        }

        public virtual DijkstraNode NearestUnvisited()
        {
            if (!HasUnvisited())
            {
                throw new GraphHasNotUnvisitedNodesException();
            }

            var minimalMark = Unvisited().Select(t => t.Mark).Min();
            return Unvisited().First(t => t.Mark == minimalMark);
        }

        private IList<DijkstraNode> Unvisited()
        {
            return Nodes.Where(t => t.IsVisited == false).Where(t => t.Mark.HasValue).ToList();
        }
    }
}
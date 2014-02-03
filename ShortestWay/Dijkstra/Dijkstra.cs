using System.Collections.Generic;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    /// <summary>
    /// Class, that finds shortest way in graph using Dijkstra algorythm.
    /// </summary>
    public class Dijkstra
    {
        /// <summary>
        /// Computes total wieghts from start node for all nodes in graph
        /// </summary>
        public Dijkstra Compute(DijkstraGraph graph)
        {
            while (graph.HasUnvisited())
            {
                var current = graph.NearestUnvisited();
                foreach (var node in graph.Linked(current))
                {
                    var weightFromCurrent = current.TotalWeigth + current.LinkWeight(node);

                    // if this way shorter
                    if (!node.TotalWeigth.HasValue || weightFromCurrent < node.TotalWeigth)
                    {
                        node.TotalWeigth = weightFromCurrent;
                        node.Previous = current;
                    }
                }

                current.IsVisited = true;
            }

            return this;
        }

        /// <summary>
        /// Returns list of nodes that represents the shortest way from start to finish node in graph.
        /// </summary>
        /// <exception cref="NoRouteFromStartToFinishException"></exception>
        public List<Node> GetShortestWay(DijkstraGraph graph)
        {
            var finish = graph.FinishNode();
            if (!finish.TotalWeigth.HasValue ||
                finish.Previous == null)
            {
                throw new NoRouteFromStartToFinishException();
            }

            var route = new List<Node>();
            while (finish != null)
            {
                route.Insert(0, finish);
                finish = finish.Previous;
            }

            return route;
        }
    }
}

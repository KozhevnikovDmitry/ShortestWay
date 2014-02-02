using System.Collections.Generic;
using ShortestWay.Exceptions;
using ShortestWay.Model;

namespace ShortestWay.Dijkstra
{
    public class Dijkstra
    {
        public Dijkstra Compute(DijkstraGraph graph)
        {
            while (graph.HasUnvisited())
            {
                var current = graph.NearestUnvisited();
                foreach (var node in graph.Linked(current))
                {
                    var weight = current.Mark + current.LinkWeight(node);
                    if (!node.Mark.HasValue || weight < node.Mark)
                    {
                        node.Mark = weight;
                        node.Previous = current;
                    }
                }

                current.IsVisited = true;
            }

            return this;
        }

        public List<Node> Find(DijkstraGraph graph)
        {
            var finish = graph.FinishNode();
            if (!finish.Mark.HasValue ||
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

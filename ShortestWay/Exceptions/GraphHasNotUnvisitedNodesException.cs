using System;

namespace ShortestWay.Exceptions
{
    public class GraphHasNotUnvisitedNodesException : ApplicationException
    {
        public GraphHasNotUnvisitedNodesException()
            :base("Graph has not unvisited nodes")
        {
            
        }
    }
}
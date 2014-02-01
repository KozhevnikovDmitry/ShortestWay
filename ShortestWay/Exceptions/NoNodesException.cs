using System;

namespace ShortestWay.Exceptions
{
    public class NoNodesException : ApplicationException
    {
        public NoNodesException()
            : base("Graph has not nodes")
        {

        }
    }
}
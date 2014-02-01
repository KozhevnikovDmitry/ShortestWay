using System;

namespace ShortestWay.Exceptions
{
    public class NoSingleStartNodeException : ApplicationException
    {
        public NoSingleStartNodeException()
            : base("Graph has not single start node")
        {

        }
    }
}
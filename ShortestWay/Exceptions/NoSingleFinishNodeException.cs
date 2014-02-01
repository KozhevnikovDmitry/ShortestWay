using System;

namespace ShortestWay.Exceptions
{
    public class NoSingleFinishNodeException : ApplicationException
    {
        public NoSingleFinishNodeException()
            : base("Graph has not single finish node")
        {

        }
    }
}
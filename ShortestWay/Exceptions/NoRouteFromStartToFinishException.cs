using System;

namespace ShortestWay.Exceptions
{
    public class NoRouteFromStartToFinishException : ApplicationException
    {
        public NoRouteFromStartToFinishException()
            :base("No route from start to finish node")
        {
            
        }
    }
}
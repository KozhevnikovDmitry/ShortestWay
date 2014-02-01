using System;

namespace ShortestWay.Exceptions
{
    public class StartNodeIsCrashedException : ApplicationException
    {
        public StartNodeIsCrashedException(int id)
            : base(string.Format("Start node is crashed [{0}]", id))
        {

        }
    }
}
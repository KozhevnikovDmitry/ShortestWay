using System;

namespace ShortestWay.Exceptions
{
    public class FinishNodeIsCrashedException : ApplicationException
    {
        public FinishNodeIsCrashedException(int id)
            : base(string.Format("Finish node is crashed [{0}]", id))
        {

        }
    }
}
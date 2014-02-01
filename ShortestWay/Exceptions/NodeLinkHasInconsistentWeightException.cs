using System;

namespace ShortestWay.Exceptions
{
    public class NodeLinkHasInconsistentWeightException : ApplicationException
    {
        public NodeLinkHasInconsistentWeightException(int id1, int id2)
            : base(string.Format("Link between nodes [{0}] and [{1}] has inconsistent weight", id1, id2))
        {

        }
    }
}
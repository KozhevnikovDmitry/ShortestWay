using System;

namespace ShortestWay.Exceptions
{
    public class NodeLinkHasNonPositiveWeightException : ApplicationException
    {
        public NodeLinkHasNonPositiveWeightException(int id1, int id2)
            : base(string.Format("Link between nodes [{0}] and [{1}] has non-positive weight", id1, id2))
        {

        }
    }
}

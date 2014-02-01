using System;

namespace ShortestWay.Exceptions
{
    public class NodeLinkIsNotBidirectionalException : ApplicationException
    {
        public NodeLinkIsNotBidirectionalException(int id1, int id2)
            : base(string.Format("Link between nodes [{0}] and [{1}] is not bidirectional", id1, id2))
        {

        }
    }
}
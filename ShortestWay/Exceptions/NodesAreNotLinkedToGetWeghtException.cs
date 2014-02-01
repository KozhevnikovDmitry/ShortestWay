using System;

namespace ShortestWay.Exceptions
{
    public class NodesAreNotLinkedToGetWeghtException : ApplicationException
    {
        public NodesAreNotLinkedToGetWeghtException(int id1, int id2)
            : base(string.Format("Nodes [{0}] and [{1}] are not linked, cannot get weight", id1, id2))
        {

        }
    }
}
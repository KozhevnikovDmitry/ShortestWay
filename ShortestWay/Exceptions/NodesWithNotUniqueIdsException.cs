using System;

namespace ShortestWay.Exceptions
{
    public class NodesWithNotUniqueIdsException : ApplicationException
    {
        public NodesWithNotUniqueIdsException(int id)
            : base(string.Format("Some nodes have the same id [{0}]", id))
        {

        }
    }
}
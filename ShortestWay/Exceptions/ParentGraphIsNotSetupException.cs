using System;

namespace ShortestWay.Exceptions
{
    public class ParentGraphIsNotSetupException : ApplicationException
    {
        public ParentGraphIsNotSetupException(int id)
            :base(string.Format("Parent graph is not setup for node [{0}]", id))
        {
            
        }
    }
}

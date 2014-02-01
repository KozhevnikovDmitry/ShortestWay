using System;

namespace ShortestWay.Exceptions
{
    public class LinksAreNotSetupException : ApplicationException
    {
        public LinksAreNotSetupException(int id)
            : base(string.Format("Links are not setup for node [{0}]", id))
        {

        }
    }
}
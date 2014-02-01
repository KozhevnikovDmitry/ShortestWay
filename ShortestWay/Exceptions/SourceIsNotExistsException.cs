using System;

namespace ShortestWay.Exceptions
{
    public class SourceIsNotExistsException : ApplicationException
    {
        public SourceIsNotExistsException(string sourcePath)
            :base(string.Format("Source is not exists on path: [{0}]", sourcePath))
        {
        }
    }
}
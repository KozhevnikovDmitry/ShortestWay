using System;

namespace ShortestWay.Exceptions
{
    public class SourceIsNotValidException : ApplicationException
    {
        public SourceIsNotValidException(Exception ex)
            :base("Source is not valid", ex)
        {
            
        }
    }
}
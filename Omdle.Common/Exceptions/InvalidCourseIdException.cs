using System;
using System.Runtime.Serialization;

namespace Omdle.Common.Exceptions
{
    public class InvalidCourseIdException : Exception
    {
        public InvalidCourseIdException()
        {
        }

        public InvalidCourseIdException(string message) : base(message)
        {
        }

        public InvalidCourseIdException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidCourseIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

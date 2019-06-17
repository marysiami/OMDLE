using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Omdle.Common.Exceptions
{
    public class RegistrationFailedException : Exception
    {
        public RegistrationFailedException()
        {
        }

        public RegistrationFailedException(string message) : base(message)
        {
        }

        public RegistrationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected RegistrationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    
    }
}

using System;
using System.Runtime.Serialization;

namespace Omdle.Common.Exceptions
{
   public class SocialSignInFailedException : Exception
    {
        public SocialSignInFailedException()
        {
        }

        public SocialSignInFailedException(string message) : base(message)
        {
        }

        public SocialSignInFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SocialSignInFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

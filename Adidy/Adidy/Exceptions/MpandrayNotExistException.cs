using System.Runtime.Serialization;

namespace Adidy.Exceptions
{
    public class MpandrayNotExistException : Exception
    {
        public MpandrayNotExistException()
        {
        }

        public MpandrayNotExistException(string? message) : base(message)
        {
        }

        public MpandrayNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected MpandrayNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

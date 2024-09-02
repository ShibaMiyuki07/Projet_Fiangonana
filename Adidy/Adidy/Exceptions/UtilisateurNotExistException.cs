using System.Runtime.Serialization;

namespace Adidy.Exceptions
{
    public class UtilisateurNotExistException : Exception
    {
        public UtilisateurNotExistException()
        {
        }

        public UtilisateurNotExistException(string? message) : base(message)
        {
        }

        public UtilisateurNotExistException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UtilisateurNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

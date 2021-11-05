
using ByteBank.Extensions;

namespace ByteBank.Exceptions
{
    public class CepException : MinhaBaseException
    {
        public CepException(string message, string title) : base(message.ToStringArray(), title, System.Net.HttpStatusCode.Conflict) { }
    }
}

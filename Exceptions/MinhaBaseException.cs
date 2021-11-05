using System;
using System.Linq;
using System.Net;

namespace ByteBank.Exceptions
{
    [Serializable]
    public class MinhaBaseException : Exception
    {
        public string[] Mensagens { get; set; }
        public string Titulo { get; set; }
        public string MensagemLog { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public MinhaBaseException(string[] mensagens, string titulo, HttpStatusCode statusCode, string mensagemLog = null) : base(mensagens.FirstOrDefault())
        {
            Mensagens = mensagens;
            Titulo = titulo;
            StatusCode = statusCode;
            MensagemLog = string.IsNullOrEmpty(mensagemLog) ? Message : mensagemLog;
        }

    }
}

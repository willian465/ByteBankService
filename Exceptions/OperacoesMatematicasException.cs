using ByteBank.Extensions;
using System;
using System.Net;

namespace ByteBank.Exceptions
{
    [Serializable]
    public class OperacoesMatematicasException : MinhaBaseException
    {
        public OperacoesMatematicasException(string[] mensagens, string title, string mensagemLog = null) : base(mensagens, title, HttpStatusCode.Conflict, mensagemLog) { }
        public OperacoesMatematicasException(string mensagem, string title, string mensagemLog = null) : base(mensagem.ToStringArray(), title, HttpStatusCode.Conflict, mensagemLog) { }
    }
}

using System.Collections.Generic;

namespace ByteBank.Request
{
    public class DadosCalcularSimilaridadeDoCossenoRequest
    {
        public List<VetorRequest> Vetores { get; set; }
        public List<CombinacaoRequest> Combinacoes { get; set; }

    }
}

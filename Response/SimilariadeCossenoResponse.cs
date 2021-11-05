using System.Collections.Generic;

namespace ByteBank.Response
{
    public class SimilariadeCossenoResponse
    {
        public int CodigoRegistro { get; set; }
        public List<DetatalheSimilariadeCossenoResponse> Resultados { get; set; }
        public List<string> Erros { get; set; }
    }
}

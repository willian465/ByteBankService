using ByteBank.Request;
using ByteBank.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IOperacoesMatematicasRepository
    {
        Task<int> RegistrarOperacao(DadosCalcularSimilaridadeDoCossenoRequest integracao, List<DetatalheSimilariadeCossenoResponse> retorno);
    }
}

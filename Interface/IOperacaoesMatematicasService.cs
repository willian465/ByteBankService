using ByteBank.Request;
using ByteBank.Response;
using System.Threading.Tasks;

namespace ByteBank.Interface
{
    public interface IOperacaoesMatematicasService
    {
        /// <summary>
        /// Método para calcular a Similaridade do Cosseno
        /// </summary>
        /// <param name="dadosSimilariadeCossenoRequests"></param>
        /// <returns></returns>
        Task<SimilariadeCossenoResponse> CalcularSimilaridadeDoCosseno(DadosCalcularSimilaridadeDoCossenoRequest dadosSimilariadeCossenoRequests);
        /// <summary>
        /// Método para calcular a Equação do Segundo Grau
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        Task<EquacaoSegundoGrauResponse> CalcularEquacaoSegundoGrau(int a, int b, int c);
    }
}

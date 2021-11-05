using ByteBank.Response.Api;
using System.Threading.Tasks;

namespace ByteBank.Interface
{
    public interface ICepService
    {
        Task<CepResponse> GetAddress(string cep);
    }
}

using ByteBank.Response.Api;
using Refit;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces.Api
{
    public interface ICepRepository
    {
        [Get("/ws/{cep}/json/")]
        Task<CepResponse> GetAddress(string cep);
    }
}

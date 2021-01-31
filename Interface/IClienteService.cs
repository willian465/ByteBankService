using ByteBank.Request;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public interface IClienteService
    {
        Task<bool> CriarCliente(CriarClienteRequest clienteRequest);
    }
}

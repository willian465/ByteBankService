using ByteBank.Model;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<ClienteModel> BuscarClientePorCodigo(int codigoCliente);
        Task<int> CriarCliente(int codigoPessoa);
        void ExluirCliente(int codigoCliente);
        Task AtualizarItemNf(int itemNf, int codigoCampus);


    }
}

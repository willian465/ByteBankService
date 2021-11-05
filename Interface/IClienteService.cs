using ByteBank.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public interface IClienteService
    {
        Task<bool> CriarCliente(CriarClienteRequest clienteRequest);
        Task<bool> AtualizarItemNf(List<ItemRequest> itemRequest);
        int Somar(int num1, int num2);
        Task<int> RegistrarContaBancaria(int codigoCliente);
    }
}

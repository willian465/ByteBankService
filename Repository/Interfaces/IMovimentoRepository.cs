using ByteBank.Argument;
using ByteBank.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IMovimentoRepository
    {
        Task<bool> RegistrarMovimentoBancario(MovimentoArgument movimento);
        Task<int> CriarTipoMovimentoBancario(string nomeMovimento);
        void DeletarMovimentoBancario(int codigoMovimento);
        Task<IEnumerable<MovimentoContaModel>> BucarMovimentosConta(int codigoConta);

    }
}

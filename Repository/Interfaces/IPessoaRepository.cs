using ByteBank.Model;
using ByteBank.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IPessoaRepository
    {
        Task<int> CriarPessoa(PessoaArgument pessoa);
        Task<bool> GerarTipoPessoa(string descricaoTipoPessoa);
        Task<IEnumerable<TipoPessoaModel>> BucarTiposPessoa();
        Task<PessoaModel> BuscarPessoaPorCodigo(int codigoPessoa);
        void BuscarPessoaPorCodigo(ItemRequest temRequest);
    }
}

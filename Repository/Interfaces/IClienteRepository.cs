using ByteBank.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Repository.Interfaces
{
    public interface IClienteRepository
    {
        void InserirPessoa(string nome, string cpf, string sexo, string dataNascimento, string email);
        Task<ClienteModel> BuscarPessoaPorCodigo(int codigoPessoa);
        void LimparTabela();
        Task<IEnumerable<ClienteModel>> BuscarPessoas();

    }
}

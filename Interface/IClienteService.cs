using ByteBank.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public interface IClienteService
    {
        Task<ClienteReponse> BuscarPessoaPorCodigo(int codigoPessoa);
        Task<IEnumerable<ClienteReponse>> BuscarPessoas();
        void InserirPessoa(string nome, string cpf, string sexo, string dataNascimento, string email);
        void LimparTabela();
        int CalcularIdade(DateTime dataNascimento);
    }
}

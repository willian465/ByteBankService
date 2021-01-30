using ByteBank.Exceptions;
using ByteBank.Repository.Interfaces;
using ByteBank.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
        /// <summary>
        /// Busca pessoa pelo código
        /// </summary>
        /// <param name="codigoPessoa"></param>
        /// <returns></returns>
        public async Task<ClienteReponse> BuscarPessoaPorCodigo(int codigoPessoa)
        {
            if (codigoPessoa == 0)
            {
                throw new ClienteException("Número inválido", "Cliente");
            }
            ClienteReponse cliente = (ClienteReponse)await _clienteRepository.BuscarPessoaPorCodigo(codigoPessoa);
            if (cliente == null)
            {
                throw new ClienteException("Cliente não encontrato", "Cliente");
            }
            return cliente;
        }
        /// <summary>
        /// Busca todas as pessoas da base
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ClienteReponse>> BuscarPessoas()
        {
            var clientes = await _clienteRepository.BuscarPessoas();
            IEnumerable<ClienteReponse> x = clientes.Select(x => (ClienteReponse)x);

            var codigoPessoas = clientes.Where(x => x.CodigoPessoa > 1).ToList();

            List<string> listaClientes = new List<string>();
            listaClientes.Add("José");
            listaClientes.Add("Maria");
            listaClientes.Add("João");
            listaClientes.Add("Carlos");
            listaClientes.Add("José");

            return x;
        }

        /// <summary>
        /// Insere pessoa
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="sexo"></param>
        /// <param name="dataNascimento"></param>
        public void InserirPessoa(string nome, string cpf, string sexo, string dataNascimento, string email)
        {
            _clienteRepository.InserirPessoa(nome, cpf, sexo, dataNascimento, email);
        }
        /// <summary>
        /// Limpa a tabela
        /// </summary>
        public void LimparTabela()
        {
            _clienteRepository.LimparTabela();
        }

        public int CalcularIdade(DateTime dataNascimento)
        {
            int idade = 0;
            if (dataNascimento < DateTime.Now)
            {
                idade = DateTime.Now.Year - dataNascimento.Year;

                if(DateTime.Now.DayOfYear < dataNascimento.DayOfYear)
                {
                    idade -= 1;
                }                
            }            
            return idade;

        }

    }
}

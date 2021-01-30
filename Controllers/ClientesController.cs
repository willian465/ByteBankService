using ByteBank.Response;
using ByteBank.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ByteBank.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        /// <summary>
        /// Método para inserir nova pessoa
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="cpf"></param>
        /// <param name="sexo"></param>
        /// <param name="dataNascimento"></param>
        [HttpPost]
        [Route("inserir")]
        public void InserirPessoa(string nome, string cpf, string sexo, string dataNascimento, string email)
        {
            _clienteService.InserirPessoa(nome, cpf, sexo, dataNascimento, email);
        }

        /// <summary>
        /// Método para limpar a tabela
        /// </summary>
        [HttpDelete]
        [Route("limpar")]
        public void Limpar()
        {
            _clienteService.LimparTabela();
        }
        /// <summary>
        /// Buscar pessoa por código
        /// </summary>
        /// <param name="codigoPessoa"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("buscar")]
        public async Task<ClienteReponse> BuscarPessoa(int codigoPessoa)
        {
            return await _clienteService.BuscarPessoaPorCodigo(codigoPessoa);
        }
        /// <summary>
        /// Buscar todas as pessoas da base
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("base")]
        public async Task<IEnumerable<ClienteReponse>> BuscasPessoas()
        {

            return await _clienteService.BuscarPessoas();

        }
        [HttpPost]
        [Route("idade")]
        public int CalcularIdade(DateTime dataNascimento)
        {
            return _clienteService.CalcularIdade(dataNascimento);

        }

    }
}

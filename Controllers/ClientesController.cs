using ByteBank.Interface;
using ByteBank.Request;
using ByteBank.Service;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ByteBank.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        private readonly IPessoaService _pessoaService;

        public ClientesController(IClienteService clienteService, IPessoaService pessoaService)
        {
            _clienteService = clienteService;
            _pessoaService = pessoaService;
        }

        /// <summary>
        /// Criar novo cliente
        /// </summary>
        /// <param name="clienterequest"></param>
        [HttpPost]
        [Route("cliente")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> CriarCliente([FromBody] CriarClienteRequest clienterequest)
        {
            return Ok(await _clienteService.CriarCliente(clienterequest));
        }

        [HttpPost]
        [Route("tipo/pessoa")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> GerarTipoPessoa(string DescricaoTipoPessoa)
        {
            return Ok(await _pessoaService.GerarTipoPessoa(DescricaoTipoPessoa));
        }

        [HttpPost]
        [Route("campus")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<bool>> AtualizarItemNf(List<ItemRequest> itemRequest) =>
            Ok(await _clienteService.AtualizarItemNf(itemRequest).ConfigureAwait(false));
    }
}

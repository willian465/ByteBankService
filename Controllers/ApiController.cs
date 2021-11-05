using ByteBank.Interface;
using ByteBank.Response.Api;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ByteBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ICepService _cepService;

        public ApiController(ICepService cepService)
        {
            _cepService = cepService;
        }

        [HttpGet]
        [Route("cep")]
        [ProducesResponseType(typeof(bool), 200)]
        public async Task<ActionResult<CepResponse>> GetAddress([FromQuery]string cep)
        {
            return Ok(await _cepService.GetAddress(cep));
        }
    }
}
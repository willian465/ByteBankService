using ByteBank.Interface;
using ByteBank.Request;
using ByteBank.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperacaoesMatematicasController : ControllerBase
    {
        private readonly IOperacaoesMatematicasService _operacaoesMatematicasService;

        public OperacaoesMatematicasController(IOperacaoesMatematicasService operacaoesMatematicasService)
        {
            _operacaoesMatematicasService = operacaoesMatematicasService;
        }

        [HttpPost]
        [Route("similaridade-cosseno")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SimilariadeCossenoResponse>>> CalcularSimilaridadeDoCosseno(DadosCalcularSimilaridadeDoCossenoRequest dadosCalcularSimilaridadeDoCosseno)
        {
            return Ok(await _operacaoesMatematicasService.CalcularSimilaridadeDoCosseno(dadosCalcularSimilaridadeDoCosseno));
        }

        [HttpPost]
        [Route("delta")]
        public async Task<ActionResult<EquacaoSegundoGrauResponse>> CalcularEquacaoSegundoGrauAsync(int a, int b, int c)
        {
            return Ok(await _operacaoesMatematicasService.CalcularEquacaoSegundoGrau(a, b, c));
        }
    }

}

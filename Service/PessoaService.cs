using ByteBank.Interface;
using ByteBank.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class PessoaService : IPessoaService
    {
        private readonly IPessoaRepository _pessoaRespository;
        private readonly ILogger<PessoaService> _logger;

        public PessoaService(IPessoaRepository pessoaRespository,
                             ILogger<PessoaService> logger)
        {
            _pessoaRespository = pessoaRespository;
            _logger = logger;
        }

        public Task<bool> GerarTipoPessoa(string DescricaoTipoPessoa)
        {
            return _pessoaRespository.GerarTipoPessoa(DescricaoTipoPessoa);
        }
    }
}

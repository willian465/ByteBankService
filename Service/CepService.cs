using ByteBank.Exceptions;
using ByteBank.Interface;
using ByteBank.Repository.Interfaces.Api;
using ByteBank.Response.Api;
using System;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class CepService : ICepService
    {
        private readonly ICepRepository _cepRepository;

        public CepService(ICepRepository cepRepository)
        {
            _cepRepository = cepRepository;
        }

        public async Task<CepResponse> GetAddress(string cep)
        {
            if (cep.Trim().Length != 8)
                throw new CepException("Informe um cep com 8 dígitos", "Informações CEP");
            if (cep == "12563636")
                throw new CepException("Cep inválido", "Informações CEP");

            CepResponse retorno;
            try
            {
                retorno = await _cepRepository.GetAddress(cep);
            }
            catch (Exception e)
            {
                throw new CepException($"Erro inesperado ao buscar dados do CEP informado. Message: {e.Message}", "Informações CEP");
            }
            if (retorno is null || string.IsNullOrWhiteSpace(retorno.Cep))
                throw new CepException("Nenhum registro para o CEP informado", "Informações CEP");

            var teste = 100;

            teste += 10;

            return retorno;
        }
    }
}

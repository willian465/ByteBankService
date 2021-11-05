using ByteBank.Exceptions;
using ByteBank.Interface;
using ByteBank.Model;
using ByteBank.Repository.Interfaces;
using ByteBank.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace ByteBank.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ClienteService> _logger;
        private readonly IOperacaoesMatematicasService _OperacaoesMatematicasService;
        private readonly IMemoryRepository _memoryRepository;
        private readonly IMemoryCache _memoryCache;

        public ClienteService(IClienteRepository clienteRepository,
                              ILogger<ClienteService> logger,
                              IPessoaRepository pessoaRepository,
                              IOperacaoesMatematicasService operacaoesMatematicasService,
                              IMemoryRepository memoryRepository)
        {
            _clienteRepository = clienteRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
            _OperacaoesMatematicasService = operacaoesMatematicasService;
            _memoryRepository = memoryRepository;
        }

        public async Task<bool> CriarCliente(CriarClienteRequest clienteRequest)
        {
            _logger.LogInformation("Inciando criação de cliente...");

            if (clienteRequest == null)
                throw new ClienteException("Dados do cliente não informado");
            if (clienteRequest.CodigoTipoPessoa == 1)
                throw new ClienteException("Teste de HTTP");


            int pessoa = await _pessoaRepository.CriarPessoa(new PessoaArgument()
            {
                CodigoTipoPessoa = clienteRequest.CodigoTipoPessoa,
                NomePessoa = clienteRequest.NomePessoa,
                DataNascimento = clienteRequest.DataNascimento,
                NumeroCpfCnpj = clienteRequest.NumeroCpfCnpj,
                Sexo = clienteRequest.Sexo,
                Email = clienteRequest.Email

            }).ConfigureAwait(false);

            int cliente = await _clienteRepository.CriarCliente(pessoa);

            return cliente > 0;
        }

        public async Task<bool> AtualizarItemNf(List<ItemRequest> listaItemRequest)
        {
            if (listaItemRequest == null)
                throw new ClienteException("Request null");


            /*List<DadosCalcularSimilaridadeDoCossenoRequest> vetorRequests = new List<DadosCalcularSimilaridadeDoCossenoRequest>()
            {
                new DadosCalcularSimilaridadeDoCossenoRequest
                    {
                        IdVetor = 1,
                        Valores = new List<int>
                        {
                            1,2,3
                        }
                    },
                new DadosCalcularSimilaridadeDoCossenoRequest
                    {
                        IdVetor = 2,
                        Valores = new List<int>
                        {
                           4,5,6
                        }
                    }
            };

            List<CombinacaoRequest> com = new List<CombinacaoRequest>
            {
                new CombinacaoRequest
            {
                IdCombinacao = 1111,
                Item1 = 1,
                Item2 = 2
            }};

            double resultado = _OperacaoesMatematicasService.CalcularSimilaridadeDoCosseno(vetorRequests, com);*/

            PessoaModel pessoa = await _pessoaRepository.BuscarPessoaPorCodigo(1);

            List<int> numeros = new List<int>(10);

            try
            {
                foreach (ItemRequest item in listaItemRequest)
                {
                    await _clienteRepository.AtualizarItemNf(item.ItemNf, item.CodigoCampus);
                }
                //listaItemRequest.ForEach(x => _clienteRepository.AtualizarItemNf(x.ItemNf, x.CodigoCampus).ConfigureAwait(true));
            }

            catch (Exception e)
            {
                _logger.LogError($"Erro ao atualizar item do campus: {e.Message}");
                throw e;
            }
            return true;
        }

        public int Somar(int num1 = 1, int num2 = 2)
        {
            if (num1 == 0 || num2 == 0)
                throw new ClienteException("Numeros inválidos");

            return num1 + num2;
        }

        public Task<int> RegistrarContaBancaria(int codigoCliente)
        {
            ContaBancariaModel conta = _memoryRepository.Buscar<ContaBancariaModel>(codigoCliente.ToString());

            if (conta != null)
                throw new ClienteException("Deu ruim");

            ContaBancariaModel contaModel = new ContaBancariaModel()
            {
                NumeroConta = new Random().Next(),
                Cliente = codigoCliente,
                Saldo = 0.0,
                DataAbertura = DateTime.Now
            };

            var result = _memoryRepository.Adicionar(contaModel, codigoCliente.ToString());

            return Task.FromResult(result.NumeroConta);
        }
        public Task<int> RegistrarContaBancaria(ContaBancariaRequest conta)
        {
            ContaBancariaModel contaModel = new ContaBancariaModel()
            {
                NumeroConta = new Random().Next(),
                Cliente = conta.Cliente,
                Saldo = 0.0,
                DataAbertura = DateTime.Now
            };

            _memoryCache.Set(1, contaModel);

            var result = _memoryCache.Get<ContaBancariaModel>(1);

            return Task.FromResult(result.NumeroConta);
        }
    }
}

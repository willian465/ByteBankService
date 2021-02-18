using ByteBank.Exceptions;
using ByteBank.Repository.Interfaces;
using ByteBank.Request;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ByteBank.Service
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IPessoaRepository _pessoaRepository;
        private readonly ILogger<ClienteService> _logger;

        public ClienteService(IClienteRepository clienteRepository,
                              ILogger<ClienteService> logger,
                              IPessoaRepository pessoaRepository)
        {
            _clienteRepository = clienteRepository;
            _pessoaRepository = pessoaRepository;
            _logger = logger;
        }

        public async Task<bool> CriarCliente(CriarClienteRequest clienteRequest)
        {
            _logger.LogInformation("Inciando criação de cliente");

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

        public async Task<bool> AtualizarItemNf(List<ItemRequest> itemRequest)
        {
            if (itemRequest == null)
                throw new ClienteException("Request null");

            Model.PessoaModel pessoa = await _pessoaRepository.BuscarPessoaPorCodigo(1);

            try
            {
                foreach (ItemRequest item in itemRequest)
                {
                    await _clienteRepository.AtualizarItemNf(item.ItemNf, item.CodigoCampus);
                }
                //itemRequest.ForEach(x => _clienteRepository.AtualizarItemNf(x.ItemNf, x.CodigoCampus).ConfigureAwait(true));
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

    }
}

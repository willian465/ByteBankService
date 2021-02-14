﻿using ByteBank.Exceptions;
using ByteBank.Repository.Interfaces;
using ByteBank.Request;
using Microsoft.Extensions.Logging;
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
                throw new ClienteException("Dados do cliente não informado", "Criação do cliente");

            var pessoa = await _pessoaRepository.CriarPessoa(new PessoaArgument()
            {
                CodigoTipoPessoa = clienteRequest.CodigoTipoPessoa,
                NomePessoa = clienteRequest.NomePessoa,
                DataNascimento = clienteRequest.DataNascimento,
                NumeroCpfCnpj = clienteRequest.NumeroCpfCnpj,
                Sexo = clienteRequest.Sexo,
                Email = clienteRequest.Email

            }).ConfigureAwait(false);

            var cliente = _clienteRepository.CriarCliente(pessoa);

            return cliente > 0;
        }

    }
}

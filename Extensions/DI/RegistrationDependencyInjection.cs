using ByteBank.Interface;
using ByteBank.Repository;
using ByteBank.Repository.Interfaces;
using ByteBank.Repository.Interfaces.Api;
using ByteBank.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;

namespace ByteBank.Extensions.DI
{
    public static class RegistrationDependencyInjection
    {
        public static void RegistrarDependencias(this IServiceCollection service, IConfiguration configuration = null)
        {
            ResgistrarRepositories(service);
            ResgistrarServices(service);
            ResgisterHtttpClient(service, configuration);
        }

        public static void ResgistrarRepositories(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IOperacoesMatematicasRepository, OperacoesMatematicasRepository>();
            services.AddScoped<IMemoryRepository, MemoryRepository>();
        }

        public static void ResgistrarServices(IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPessoaService, PessoaService>();
            services.AddScoped<IOperacaoesMatematicasService, OperacoesMatematicasService>();
            services.AddScoped<ICepService, CepService>();
        }
        private static void ResgisterHtttpClient(IServiceCollection services, IConfiguration configuration = null)
        {
            services
                .AddRefitClient<ICepRepository>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://viacep.com.br"));
        }

    }
}

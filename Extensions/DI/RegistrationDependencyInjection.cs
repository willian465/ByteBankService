using ByteBank.Interface;
using ByteBank.Repository;
using ByteBank.Repository.Interfaces;
using ByteBank.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ByteBank.Extensions.DI
{
    public static class RegistrationDependencyInjection
    {
        public static void Registrar(this IServiceCollection service, IConfiguration configuration = null)
        {
            ResgistrarRepositories(service);
            ResgistrarServices(service);
        }

        public static void ResgistrarRepositories(IServiceCollection services)
        {
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();


        }
        public static void ResgistrarServices(IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPessoaService, PessoaService>();
        }

    }
}

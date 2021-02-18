using ByteBank.Extensions.DI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace ByteBank
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Este método é chamado pelo tempo de execução. Use este método para adicionar serviços ao contêiner.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddCors(options =>
            options.AddDefaultPolicy(
                builder => builder.AllowAnyOrigin()));

            services.Registrar(_configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ByteBank", Version = "v1" });
            });
        }

        // Este método é chamado pelo tempo de execução. Use este método para configurar o pipeline de solicitação HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            string routerPrefix = "";
            app.UseStaticFiles();
            UseSwagger(app, routerPrefix);


        }
        private void UseSwagger(IApplicationBuilder app, string routerPrefix)
        {
            app.UseSwagger(
                c => c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    //s.Paths = routerPrefix;
                    swaggerDoc.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{routerPrefix}" },
                                                                   new OpenApiServer { Url = $"https://{httpReq.Host.Value}{routerPrefix}" }
                                                                 };

                }));
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"{routerPrefix}/swagger/v1/swagger.json", $"{_configuration.GetSection("Application").GetValue<string>("EndpointName")}");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}

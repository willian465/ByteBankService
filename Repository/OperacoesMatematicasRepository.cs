using ByteBank.Repository.Base;
using ByteBank.Repository.Interfaces;
using ByteBank.Request;
using ByteBank.Response;
using Dapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ByteBank.Repository
{
    public class OperacoesMatematicasRepository : Postgree, IOperacoesMatematicasRepository
    {
        private readonly ILogger<OperacoesMatematicasRepository> _logger;

        public OperacoesMatematicasRepository(ILogger<OperacoesMatematicasRepository> logger)
        {
            _logger = logger;
        }

        public async Task<int> RegistrarOperacao(DadosCalcularSimilaridadeDoCossenoRequest integracao, List<DetatalheSimilariadeCossenoResponse> retorno)
        {
            const string sql = @"INSERT INTO public.operacoes_matematicas
                                                   (json_integracao, json_retorno)
                                             VALUES(@JsonIntegracao, @JsonRetorno)
                                           returning cod_operacao";


            using (IDbConnection connection = GetConnection())
            {
                int codigo = await connection.ExecuteScalarAsync<int>(sql, new
                {
                    JsonIntegracao = JsonConvert.SerializeObject(integracao),
                    JsonRetorno = JsonConvert.SerializeObject(retorno)
                });

                return codigo;
            }
        }
    }
}

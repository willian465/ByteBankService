using ByteBank.Argument;
using ByteBank.Model;
using ByteBank.Repository.Base;
using ByteBank.Repository.Interfaces;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ByteBank.Repository
{
    public class MovimentoRepository : Postgree, IMovimentoRepository
    {
        private readonly ILogger<MovimentoRepository> _logger;

        public MovimentoRepository(ILogger<MovimentoRepository> logger)
        {
            _logger = logger;
        }
        public async Task<IEnumerable<MovimentoContaModel>> BucarMovimentosConta(int codigoConta)
        {
            const string sql = @"   SELECT COD_MOVIMENTO CodigoMovimento , 
                                           COD_CONTA CodigoConta, 
                                           tm.dsc_tpo_movimento Movimento, 
                                           SALDO_INICIAL SaldoInicial, 
                                           SALDO_ATUAL SaldoAtual, 
                                           DAT_MOVIMENTO DataMovimento
                                    FROM HIST_MOVIMENTO_CONTA HMC
                                             inner join tipo_movimento tm on HMC.cod_tpo_movimento = TM.cod_tpo_movimento 
                                    where cod_conta = @CodigoConta";

            using (IDbConnection connection = GetConnection())
            {
                return (await connection.QueryAsync<MovimentoContaModel>(sql, new
                {
                    CodigoConta = codigoConta

                }));
            }
        }

        public async Task<int> CriarTipoMovimentoBancario(string nomeMovimento)
        {
            const string sql = @"INSERT INTO tipo_movimento
                                        (dsc_tpo_movimento)
                                        VALUES(@NomeMovimento)";


            using (IDbConnection connection = GetConnection())
            {
                int codigo = await connection.ExecuteScalarAsync<int>(sql, nomeMovimento).ConfigureAwait(false);
                return codigo;
            }
        }

        public void DeletarMovimentoBancario(int codigoMovimento)
        {

            try
            {
                using (IDbConnection connection = GetConnection())
                {
                    const string sql = @" DELETE FROM hist_movimento_conta
                                           WHERE cod_movimento = @CodigoMovimento";
                    connection.ExecuteScalarAsync(sql, codigoMovimento).ConfigureAwait(false);

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Ocorreu um erro ao deletar o movimento: {@CodigoMovimento} - {@Message}", e.Message);
                throw e;

            }

        }

        public async Task<bool> RegistrarMovimentoBancario(MovimentoArgument movimento)
        {
            const string sql = @"   INSERT
	                                    INTO
	                                    HIST_MOVIMENTO_CONTA (COD_CONTA, 
	                                    COD_TPO_MOVIMENTO, 
	                                    SALDO_INICIAL, 
	                                    SALDO_ATUAL)
                                    VALUES(@CodigoConta, @CodigoMovimento, @saldo_inicial, @saldo_atual) 
                                    returning cod_movimento";

            using (IDbConnection connection = GetConnection())
            {
                int registrou = await connection.ExecuteScalarAsync<int>(sql, movimento).ConfigureAwait(false);
                return registrou > 0;
            }
        }
    }
}

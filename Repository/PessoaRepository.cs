using ByteBank.Model;
using ByteBank.Repository.Base;
using ByteBank.Repository.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Repository
{
    public class PessoaRepository : Postgree, IPessoaRepository
    {
        /// <summary>
        /// Método para buscar tipos de pessoa cadastradas
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TipoPessoaModel>> BucarTiposPessoa()
        {
            const string sql = @"   SELECT COD_TPO_PESSOA CodigoTipoPessoa, 
                                           DSC_TIPO_PESSOA DescricaoTipoPessoa, 
                                           DAT_CADASTRO DataCadastro
                                      FROM TIPO_PESSOA PS
                                     WHERE PS.COD_TPO_PESSOA = @CodigoTipoPessoa";

            using (IDbConnection connection = GetConnection())
            {
                return (await connection.QueryAsync<TipoPessoaModel>(sql).ConfigureAwait(false));
            }


        }

        /// <summary>
        /// Método para buscar pessoa por código
        /// </summary>
        /// <param name="codigoPessoa"></param>
        /// <returns></returns>
        public async Task<PessoaModel> BuscarPessoaPorCodigo(int codigoPessoa)
        {
            string sql = @"     SELECT COD_PESSOA CodigoPessoa, 
                                       TP.DSC_TIPO_PESSOA DescricaoTipoPessoa, 
                                       NOM_PESSOA NomePessoa, 
                                       DAT_NASCIMENTO DataNascimento, 
                                       NUM_CPF_CNPJ NumeroCpfCnpj, 
                                       IND_SEXO Sexo,
                                       EMAIL Email
                                FROM PESSOA P 
      	                                INNER JOIN TIPO_PESSOA TP ON TP.COD_TPO_PESSOA = P.COD_TPO_PESSOA 
                                WHERE P.COD_PESSOA = @CodigoPessoa";

            using (IDbConnection connection = GetConnection())
            {
                return (await connection.QueryAsync<PessoaModel>(sql, new
                {
                    CodigoPessoa = codigoPessoa

                })).FirstOrDefault();
            }
        }
        /// <summary>
        /// Método para criar nova pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns></returns>
        public async Task<int> CriarPessoa(PessoaArgument pessoa)
        {


            try
            {
                using (IDbConnection connection = GetConnection())
                {

                    const string sql = @"INSERT
	                                INTO
	                                PESSOA (COD_TPO_PESSOA, NOM_PESSOA, DAT_NASCIMENTO, NUM_CPF_CNPJ, IND_SEXO, EMAIL)
                                  VALUES(@CodigoTipoPessoa, 
                                         @NomePessoa, 
                                         @DataNascimento, 
                                         @NumeroCpfCnpj, 
                                         @Sexo, 
                                         @Email) RETURNING COD_PESSOA";

                    int codigoPessoa = await connection.ExecuteScalarAsync<int>(sql, pessoa).ConfigureAwait(false);
                    return codigoPessoa;
                }
            }
            catch (Exception e)
            {
                throw e;
            }



        }
        /// <summary>
        /// Método para gerar nova pessoa
        /// </summary>
        /// <param name="DescricaoTipoPessoa"></param>
        /// <returns></returns>
        public async Task<bool> GerarTipoPessoa(string descricaoTipoPessoa)
        {
            try
            {
                const string sql = @"INSERT INTO tipo_pessoa
                                        (dsc_tipo_pessoa)
                                  VALUES(@DescricaoTipoPessoa)";

                using (IDbConnection connection = GetConnection())
                {
                    int gerou = await connection.ExecuteAsync(sql, new { DescricaoTipoPessoa = descricaoTipoPessoa }).ConfigureAwait(false);
                    return gerou > 0;
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }

    }
}


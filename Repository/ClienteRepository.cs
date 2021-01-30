using ByteBank.Model;
using ByteBank.Repository.Base;
using ByteBank.Repository.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Repository
{
    public class ClienteRepository : Postgree, IClienteRepository
    {
        public void InserirPessoa(string nome, string cpf, string sexo, string dataNascimento, string email)
        {
            string sql = @"INSERT INTO PESSOA (NOME_PESSOA,
                                               DATA_NASCIMENTO,
                                               CPF,
                                               SEXO,
                                               EMAIL) 
                                        VALUES(@Nome, @DataNascimento, @Cpf, @Sexo, @Email)";


            using (IDbConnection connection = GetConnection())

                connection.ExecuteScalar(sql, new
                {
                    Nome = nome,
                    Cpf = cpf,
                    Sexo = sexo,
                    DataNascimento = dataNascimento,
                    Email = email
                });

        }

        public async Task<ClienteModel> BuscarPessoaPorCodigo(int codigoPessoa)
        {
            string sql = @"SELECT CODIGO_PESSOA CodigoPessoa,                                                            
                                             NOME_PESSOA NomePessoa,
                                             DATA_NASCIMENTO DataNascimento,
                                             CPF CpfPessoa,
                                             SEXO Sexo,
                                             EMAIL Email
                                        FROM PESSOA WHERE CODigo_PESSOA = @CODIGO";

            using (IDbConnection connection = GetConnection())
            {
                return (await connection.QueryAsync<ClienteModel>(sql, new
                {
                    CODIGO = codigoPessoa
                })).FirstOrDefault();
            }
        }

        public void LimparTabela()
        {
            string sql = @"DELETE FROM PESSOA";

            using (IDbConnection connection = GetConnection())

                connection.ExecuteScalar(sql);
        }

        public async Task<IEnumerable<ClienteModel>> BuscarPessoas()
        {
            string sql = @"SELECT CODIGO_PESSOA CodigoPessoa,                                                            
                                  NOME_PESSOA NomePessoa,
                                  DATA_NASCIMENTO DataNascimento,
                                  CPF CpfPessoa,
                                  SEXO Sexo,
                                  EMAIL Email
                             FROM PESSOA";

            using IDbConnection connection = GetConnection();

            return await connection.QueryAsync<ClienteModel>(sql);
        }
    }

}

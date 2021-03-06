﻿using ByteBank.Model;
using ByteBank.Repository.Base;
using ByteBank.Repository.Interfaces;
using Dapper;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ByteBank.Repository
{
    public class ClienteRepository : Postgree, IClienteRepository
    {
        public async Task<ClienteModel> BuscarClientePorCodigo(int codigoCliente)
        {
            const string sql = @"   SELECT C.COD_CLIENTE CodigoCliente, 
                                           C.COD_PESSOA CodigoPessoa, 
                                           C.DAT_CADASTRO DataCadastro
                                      FROM CLIENTE C
                                     WHERE C.COD_CLIENTE = @CodigoCliente";

            using (IDbConnection connection = GetConnection())
            {
                return (await connection.QueryAsync<ClienteModel>(sql, new
                {
                    CodigoCliente = codigoCliente

                })).FirstOrDefault();
            }
        }

        public async Task<int> CriarCliente(int codigoPessoa)
        {

            try
            {
                const string sql = @"INSERT INTO cliente (cod_pessoa)
                                        VALUES (@CodigoPessoa) 
                                        returning cod_cliente";

                using (IDbConnection connection = GetConnection())
                {
                    int codigoCliente = await connection.ExecuteScalarAsync<int>(sql, new { CodigoPessoa = codigoPessoa });
                    return codigoCliente;
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public void ExluirCliente(int codigoCliente)
        {
            const string sql = @"DELETE FROM CLIENTE C
                                  WHERE C.COD_CLIENTE = @CodigoCliente";

            using (IDbConnection connection = GetConnection())
            {
                connection.ExecuteScalarAsync(sql, codigoCliente).ConfigureAwait(false);

            }
        }

        public async Task AtualizarItemNf(int itemNf, int codigoCampus)
        {
            const string sql = @"UPDATE campus 
                                    SET ITEM_NF = @ItemNf
                                  WHERE COD_CAMPUS = @CodigoCampus";

            using (IDbConnection connection = GetConnection())
            {
                await connection.ExecuteAsync(sql, new { ItemNf = itemNf, CodigoCampus = codigoCampus });

            }
        }
    }
}
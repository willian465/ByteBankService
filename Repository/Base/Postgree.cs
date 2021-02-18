using Npgsql;
using System;
using System.Data;

namespace ByteBank.Repository.Base
{
    public class Postgree
    {
        internal IDbConnection GetConnection()
        {
            IDbConnection dbConnection;

            try
            {
                dbConnection = new NpgsqlConnection("User ID = postgres; Password = banco123; Host = localhost; Port = 5432; Database = ByteBankDatabase");
                dbConnection.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
            return dbConnection;
        }

    }
}

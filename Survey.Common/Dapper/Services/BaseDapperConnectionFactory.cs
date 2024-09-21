using Survey.Common.Dapper.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace Survey.Common.Dapper.Services
{
    public class BaseDapperConnectionFactory : IDapperConnectionFactory
    {
        private readonly string _connectionString;

        public BaseDapperConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
    }
}   
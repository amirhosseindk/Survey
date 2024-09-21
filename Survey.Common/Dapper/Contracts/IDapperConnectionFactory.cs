using System.Data;

namespace Survey.Common.Dapper.Contracts
{
    public interface IDapperConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
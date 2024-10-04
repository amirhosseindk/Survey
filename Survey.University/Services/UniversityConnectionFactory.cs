using Survey.Common.Dapper.Services;
using Survey.University.Contracts;

namespace Survey.University.Services
{
    public class UniversityReadConnectionFactory : BaseDapperConnectionFactory, IUniversityReadConnectionFactory
    {
        public UniversityReadConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }

    public class UniversityWriteConnectionFactory : BaseDapperConnectionFactory, IUniversityWriteConnectionFactory
    {
        public UniversityWriteConnectionFactory(string connectionString) : base(connectionString)
        {
        }
    }
}
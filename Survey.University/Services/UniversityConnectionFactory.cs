using Survey.Common.Dapper.Services;
using Survey.University.Contracts;

namespace Survey.University.Services
{
    public class UniversityConnectionFactory : BaseDapperConnectionFactory, IUniversityConnectionFactory
    {
        public UniversityConnectionFactory(string connectionString) : base(connectionString)
        {
            
        }
    }
}
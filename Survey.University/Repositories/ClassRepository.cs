using Dapper;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IUniversityConnectionFactory _dbConnectionFactory;

        public ClassRepository(IUniversityConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(Class @class)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, @class);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Class>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Class>(sql);
        }

        public async Task<Class> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Class>(sql, new { Id = id });
        }

        public async Task UpdateAsync(Class @class)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, @class);
        }

        private string GetCreateSQL()
        {
            return @"
            INSERT INTO Classes (Name, CourseId)
            VALUES (@Name, @CourseId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Classes WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM Classes";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM Classes WHERE Id = @Id";
        }

        private string GetUpdateSQL()
        {
            return @"
            UPDATE Classes
            SET Name = @Name,
                CourseId = @CourseId
            WHERE Id = @Id
        ";
        }
    }
}
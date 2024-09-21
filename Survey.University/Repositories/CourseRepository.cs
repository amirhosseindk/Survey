using Dapper;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IUniversityConnectionFactory _dbConnectionFactory;

        public CourseRepository(IUniversityConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(Course course)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, course);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Course>(sql);
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Course>(sql, new { Id = id });
        }

        public async Task UpdateAsync(Course course)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, course);
        }

        private string GetCreateSQL()
        {
            return @"
            INSERT INTO Courses (Name, ProfessorId)
            VALUES (@Name, @ProfessorId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Courses WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM Courses";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM Courses WHERE Id = @Id";
        }

        private string GetUpdateSQL()
        {
            return @"
            UPDATE Courses
            SET Name = @Name,
                ProfessorId = @ProfessorId
            WHERE Id = @Id
        ";
        }
    }
}
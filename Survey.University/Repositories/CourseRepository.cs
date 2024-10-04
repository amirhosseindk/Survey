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

        public async Task<int> CreateAsync(CourseRepoModel course)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, course);
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<CourseRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<CourseRepoModel>(sql);
        }

        public async Task<CourseRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<CourseRepoModel>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(CourseRepoModel course)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, course);
            return rowsAffected > 0;
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId)
        {
            var sql = GetClassesByCourseIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<(int ClassId, string ClassName)>(sql, new { CourseId = courseId });

            return result.ToDictionary(x => x.ClassId, x => x.ClassName);
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName)
        {
            string sql = GetClassesByCourseNameSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<(int ClassId, string ClassName)>(sql, new { CourseName = courseName });

            return result.ToDictionary(x => x.ClassId, x => x.ClassName);
        }

        private string GetClassesByCourseNameSQL()
        {
            return @"
            SELECT cl.Id as ClassId, cl.Name as ClassName
            FROM Classes cl
            INNER JOIN Courses c ON cl.CourseId = c.Id
            WHERE c.Name = @CourseName;
            ";
        }

        private string GetClassesByCourseIdSQL()
        {
            return @"
            SELECT cl.Id as ClassId, cl.Name as ClassName
            FROM Classes cl
            WHERE cl.CourseId = @CourseId;
            ";
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
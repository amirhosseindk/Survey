using Dapper;
using Microsoft.Extensions.Logging;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly IUniversityReadConnectionFactory _readConnectionFactory;
        private readonly IUniversityWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<CourseRepository> _logger;

        public CourseRepository(
            IUniversityReadConnectionFactory readConnectionFactory,
            IUniversityWriteConnectionFactory writeConnectionFactory,
            ILogger<CourseRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(CourseRepoModel course)
        {
            var sql = GetCreateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var id = await connection.QuerySingleAsync<int>(sql, course);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating course - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateAsync(CourseRepoModel course)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, course);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating course - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting course by ID {id} - {ex.Message}");
                throw;
            }
        }

        public async Task<CourseRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<CourseRepoModel>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting course by ID {id} - {ex.Message}");
                throw;
            }
        }

        public async Task<CourseRepoModel> GetByNameAsync(string name)
        {
            var sql = GetByNameSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<CourseRepoModel>(sql, new { Name = name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting course by Name {name} - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<CourseRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<CourseRepoModel>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting all courses - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<CourseRepoModel>> GetAllByProfessorIdAsync(string professorId)
        {
            var sql = GetAllByProfessorIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<CourseRepoModel>(sql, new { ProfessorId = professorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting all courses - {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId)
        {
            var sql = GetClassesByCourseIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                var result = await connection.QueryAsync<(int ClassId, string ClassName)>(sql, new { CourseId = courseId });
                return result.ToDictionary(x => x.ClassId, x => x.ClassName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting classes by course by ID {courseId} - {ex.Message}");
                throw;
            };
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName)
        {
            var sql = GetClassesByCourseNameSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                var result = await connection.QueryAsync<(int ClassId, string ClassName)>(sql, new { CourseName = courseName });
                return result.ToDictionary(x => x.ClassId, x => x.ClassName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting classes by course by Name {courseName} - {ex.Message}");
                throw;
            }
        }

        public async Task<CourseRepoModel> GetByClassIdAsync(int id)
        {
            var sql = GetByClassIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<CourseRepoModel>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting course by ClassID {id} - {ex.Message}");
                throw;
            }
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

        private string GetAllByProfessorIdSQL()
        {
            return "SELECT * FROM Courses WHERE ProfessorId = @ProfessorId";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM Courses WHERE Id = @Id";
        }

        private string GetByNameSQL()
        {
            return "SELECT * FROM Courses WHERE Name = @Name";
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

        private string GetByClassIdSQL()
        {
            return @"SELECT c.Id,c.Name,c.ProfessorId FROM Courses c INNER JOIN Classes cl ON c.Id = cl.CourseId WHERE cl.Id = @Id;";
        }
    }
}
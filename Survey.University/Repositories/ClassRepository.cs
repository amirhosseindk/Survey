using Dapper;
using Microsoft.Extensions.Logging;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly IUniversityConnectionFactory _dbConnectionFactory;
        private readonly ILogger<ClassRepository> _logger;

        public ClassRepository(IUniversityConnectionFactory dbConnectionFactory, ILogger<ClassRepository> logger)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(ClassRepoModel @class)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, @class);
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<ClassRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<ClassRepoModel>(sql);
        }

        public async Task<ClassRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ClassRepoModel>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(ClassRepoModel @class)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, @class);
            return rowsAffected > 0;
        }

        public async Task<int> AddStudentToClassAsync(ClassStudentRepoModel classStudents)
        {
            var sql = GetAddStudentToClassSQL();
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();
                var id = await connection.ExecuteAsync(sql, classStudents);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding Student : {studentId} to Class : {classId}", classStudents.StudentId, classStudents.ClassId);
                throw;
            }
        }

        public async Task<bool> RemoveStudentFromClassAsync(int classId, string studentId)
        {
            var sql = GetRemoveStudentFromClassSQL();
            try
            {
                using var connection = _dbConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, new { ClassesId = classId, StudentsId = studentId });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting Student : {studentId} from Class : {classId}", studentId, classId);
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassIdAsync(int classId)
        {
            var sql = GetStudentsByClassIdSQL();

            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<(string StudentId, string StudentName)>(sql, new { ClassId = classId });

            return result.ToDictionary(x => x.StudentId, x => x.StudentName);
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassNameAsync(string className)
        {
            var sql = GetStudentsByClassNameSQL();

            using var connection = _dbConnectionFactory.CreateConnection();
            var result = await connection.QueryAsync<(string StudentId, string StudentName)>(sql, new { ClassName = className });

            return result.ToDictionary(x => x.StudentId, x => x.StudentName);
        }

        private string GetStudentsByClassNameSQL()
        {
            return @"
                    SELECT cs.StudentsId as StudentId, u.UserName as StudentName
                    FROM ClassStudents cs
                    INNER JOIN Classes cl ON cs.ClassesId = cl.Id
                    INNER JOIN AspNetUsers u ON cs.StudentsId = u.Id
                    WHERE cl.Name = @ClassName;
                    ";
        }

        private string GetStudentsByClassIdSQL()
        {
            return @"
                SELECT cs.StudentsId as StudentId, u.UserName as StudentName
                FROM ClassStudents cs
                INNER JOIN AspNetUsers u ON cs.StudentsId = u.Id
                WHERE cs.ClassesId = @ClassId;
                ";
        }

        private string GetAddStudentToClassSQL()
        {
            return @"
                INSERT INTO ClassStudents (ClassesId, StudentsId)
                VALUES (@ClassId, @StudentId);
                ";
        }

        private string GetRemoveStudentFromClassSQL()
        {
            return "DELETE FROM ClassStudents WHERE ClassesId = @ClassId AND StudentsId = @StudentId";
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
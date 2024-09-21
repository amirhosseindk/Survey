using Dapper;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Repositories
{
    public class ClassStudentsRepository : IClassStudentsRepository
    {
        private readonly IUniversityConnectionFactory _dbConnectionFactory;

        public ClassStudentsRepository(IUniversityConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(ClassStudents classStudents)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.ExecuteAsync(sql, classStudents);
            return id;
        }

        public async Task DeleteAsync(int classId, int studentId)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { ClassesId = classId, StudentsId = studentId });
        }

        public async Task<IEnumerable<ClassStudents>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<ClassStudents>(sql);
        }

        public async Task<ClassStudents> GetByIdAsync(int classId, int studentId)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<ClassStudents>(sql, new { ClassesId = classId, StudentsId = studentId });
        }

        public async Task UpdateAsync(ClassStudents classStudents)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, classStudents);
        }

        private string GetCreateSQL()
        {
            return @"
            INSERT INTO ClassStudents (ClassesId, StudentsId)
            VALUES (@ClassesId, @StudentsId);
        ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM ClassStudents WHERE ClassesId = @ClassesId AND StudentsId = @StudentsId";
        }

        private string GetGetAllSQL()
        {
            return "SELECT * FROM ClassStudents";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM ClassStudents WHERE ClassesId = @ClassesId AND StudentsId = @StudentsId";
        }

        private string GetUpdateSQL()
        {
            return @"
            UPDATE ClassStudents
            SET ClassesId = @ClassesId,
                StudentsId = @StudentsId
            WHERE ClassesId = @ClassesId AND StudentsId = @StudentsId
        ";
        }
    }
}
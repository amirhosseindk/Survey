using Dapper;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Contracts;

namespace Survey.Questionnaires.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public QuestionnaireRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(Questionnaire questionnaire)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, questionnaire);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Questionnaire>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Questionnaire>(sql);
        }

        public async Task<Questionnaire> GetByIdAsync(int id)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Questionnaire>(sql, new { Id = id });
        }

        public async Task UpdateAsync(Questionnaire questionnaire)
        {
            var sql = GetUpdateSQL();

            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, questionnaire);
        }

        private string GetCreateSQL()
        {
            return @"
            INSERT INTO Questionnaires (Title, ClassId, ProfessorId)
            VALUES (@Title, @ClassId, @ProfessorId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Questionnaires WHERE Id = @Id";
        }

        private string GetGetAllSQL()
        {
            return "SELECT * FROM Questionnaires";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM Questionnaires WHERE Id = @Id";
        }

        private string GetUpdateSQL()
        {
            return @"
            UPDATE Questionnaires
            SET Title = @Title,
                ClassId = @ClassId,
                ProfessorId = @ProfessorId
            WHERE Id = @Id
        ";
        }
    }
}
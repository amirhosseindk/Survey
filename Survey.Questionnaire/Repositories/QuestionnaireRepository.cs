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

        public async Task<int> CreateAsync(QuestionnaireRepoModel questionnaire)
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

        public async Task<IEnumerable<QuestionnaireRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<QuestionnaireRepoModel>(sql);
        }

        public async Task<QuestionnaireRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<QuestionnaireRepoModel>(sql, new { Id = id });
        }

        public async Task<bool> UpdateAsync(QuestionnaireRepoModel questionnaire)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, questionnaire);
            return rowsAffected > 0;
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

        private string GetAllSQL()
        {
            return "SELECT * FROM Questionnaires";
        }

        private string GetByIdSQL()
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
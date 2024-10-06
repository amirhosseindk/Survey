using Dapper;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Contracts;

namespace Survey.Questionnaires.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public QuestionRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(QuestionRepoModel question)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, question);
            return id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
            return rowsAffected > 0;
        }

        public async Task<IEnumerable<QuestionRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<QuestionRepoModel>(sql);
        }

        public async Task<QuestionRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<QuestionRepoModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<QuestionRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<QuestionRepoModel>(sql, new { QuestionnaireId = questionnaireId });
        }

        public async Task<bool> UpdateAsync(QuestionRepoModel question)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var rowsAffected = await connection.ExecuteAsync(sql, question);
            return rowsAffected > 0;
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO Questions (Rank, Type, Title, QuestionnaireId)
                VALUES (@Rank, @Type, @Title, @QuestionnaireId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Questions WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM Questions";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM Questions WHERE Id = @Id";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM Questions WHERE QuestionnaireId = @QuestionnaireId ORDER BY Rank";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE Questions
                SET Rank = @Rank,
                    Type = @Type,
                    Title = @Title,
                    QuestionnaireId = @QuestionnaireId
                WHERE Id = @Id
            ";
        }
    }
}
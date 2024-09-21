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

        public async Task<int> CreateAsync(Question question)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, question);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Question>(sql);
        }

        public async Task<Question> GetByIdAsync(int id)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<Question>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Question>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetGetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<Question>(sql, new { QuestionnaireId = questionnaireId });
        }

        public async Task UpdateAsync(Question question)
        {
            var sql = GetUpdateSQL();

            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, question);
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

        private string GetGetAllSQL()
        {
            return "SELECT * FROM Questions";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM Questions WHERE Id = @Id";
        }

        private string GetGetByQuestionnaireIdSQL()
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
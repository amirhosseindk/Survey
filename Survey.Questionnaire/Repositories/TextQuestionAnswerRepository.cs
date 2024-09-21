using Dapper;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class TextQuestionAnswerRepository : ITextQuestionAnswerRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public TextQuestionAnswerRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(TextQuestionAnswer answer)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, answer);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<TextQuestionAnswer>(sql);
        }

        public async Task<TextQuestionAnswer> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<TextQuestionAnswer>(sql, new { Id = id });
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<TextQuestionAnswer>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(TextQuestionAnswer answer)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
        }

        public async Task<IEnumerable<TextQuestionAnswer>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<TextQuestionAnswer>(sql, new { QuestionnaireId = questionnaireId });
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO TextQuestionAnswers (QuestionnaireId, QuestionId, AnswerText, StudentId, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @AnswerText, @StudentId, @FillDateTime);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM TextQuestionAnswers WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM TextQuestionAnswers";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE TextQuestionAnswers
                SET QuestionnaireId = @QuestionnaireId,
                    QuestionId = @QuestionId,
                    AnswerText = @AnswerText,
                    StudentId = @StudentId,
                    FillDateTime = @FillDateTime
                WHERE Id = @Id
            ";
        }

        private static string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
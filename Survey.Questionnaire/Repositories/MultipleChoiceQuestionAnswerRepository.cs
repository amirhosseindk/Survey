using Dapper;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class MultipleChoiceQuestionAnswerRepository : IMultipleChoiceQuestionAnswerRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public MultipleChoiceQuestionAnswerRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(MultipleChoiceQuestionAnswer answer)
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

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceQuestionAnswer>(sql);
        }

        public async Task<MultipleChoiceQuestionAnswer> GetByIdAsync(int id)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MultipleChoiceQuestionAnswer>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswer>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetGetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceQuestionAnswer>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(MultipleChoiceQuestionAnswer answer)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceQuestionAnswers (QuestionnaireId, QuestionId, AnswerOptionId, StudentId, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @AnswerOptionId, @StudentId, @FillDateTime);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM MultipleChoiceQuestionAnswers WHERE Id = @Id";
        }

        private string GetGetAllSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE Id = @Id";
        }

        private string GetGetByQuestionIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE MultipleChoiceQuestionAnswers
                SET QuestionnaireId = @QuestionnaireId,
                    QuestionId = @QuestionId,
                    AnswerOptionId = @AnswerOptionId,
                    StudentId = @StudentId,
                    FillDateTime = @FillDateTime
                WHERE Id = @Id
            ";
        }
    }
}
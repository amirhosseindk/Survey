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

        public async Task<int> CreateAsync(MultipleChoiceQuestionAnswerRepoModel answer)
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

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql);
        }

        public async Task<MultipleChoiceQuestionAnswerRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(MultipleChoiceQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
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

        private string GetAllSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
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

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
using Dapper;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class RangeQuestionAnswerRepository : IRangeQuestionAnswerRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public RangeQuestionAnswerRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(RangeQuestionAnswerRepoModel answer)
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

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql);
        }

        public async Task<RangeQuestionAnswerRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<RangeQuestionAnswerRepoModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(RangeQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO RangeQuestionAnswers (QuestionnaireId, QuestionId, StudentId, AnswerValue, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @StudentId, @AnswerValue, @FillDateTime);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM RangeQuestionAnswers WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE RangeQuestionAnswers
                SET QuestionnaireId = @QuestionnaireId,
                    QuestionId = @QuestionId,
                    StudentId = @StudentId,
                    AnswerValue = @AnswerValue,
                    FillDateTime = @FillDateTime
                WHERE Id = @Id
            ";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
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

        public async Task<int> CreateAsync(RangeQuestionAnswer answer)
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

        public async Task<IEnumerable<RangeQuestionAnswer>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<RangeQuestionAnswer>(sql);
        }

        public async Task<RangeQuestionAnswer> GetByIdAsync(int id)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<RangeQuestionAnswer>(sql, new { Id = id });
        }

        public async Task<IEnumerable<RangeQuestionAnswer>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetGetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<RangeQuestionAnswer>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(RangeQuestionAnswer answer)
        {
            var sql = GetUpdateSQL();

            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
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

        private string GetGetAllSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE Id = @Id";
        }

        private string GetGetByQuestionIdSQL()
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
    }
}
using Dapper;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class DegreeQuestionAnswerRepository : IDegreeQuestionAnswerRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public DegreeQuestionAnswerRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(DegreeQuestionAnswer answer)
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

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<DegreeQuestionAnswer>(sql);
        }

        public async Task<DegreeQuestionAnswer> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<DegreeQuestionAnswer>(sql, new { Id = id });
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<DegreeQuestionAnswer>(sql, new { QuestionId = questionId });
        }

        public async Task UpdateAsync(DegreeQuestionAnswer answer)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, answer);
        }

        public async Task<IEnumerable<DegreeQuestionAnswer>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<DegreeQuestionAnswer>(sql, new { QuestionnaireId = questionnaireId });
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO DegreeQuestionAnswers (QuestionnaireId, QuestionId, StudentId, AnswerValue, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @StudentId, @AnswerValue, @FillDateTime);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM DegreeQuestionAnswers WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE DegreeQuestionAnswers
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
            return "SELECT * FROM DegreeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
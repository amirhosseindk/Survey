using Dapper;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class MultipleChoiceOptionRepository : IMultipleChoiceOptionRepository
    {
        private readonly IQuestionnaireConnectionFactory _dbConnectionFactory;

        public MultipleChoiceOptionRepository(IQuestionnaireConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<int> CreateAsync(MultipleChoiceOption option)
        {
            var sql = GetCreateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            var id = await connection.QuerySingleAsync<int>(sql, option);
            return id;
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<MultipleChoiceOption>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetGetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceOption>(sql, new { QuestionId = questionId });
        }

        public async Task<MultipleChoiceOption> GetByIdAsync(int id)
        {
            var sql = GetGetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MultipleChoiceOption>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MultipleChoiceOption>> GetAllAsync()
        {
            var sql = GetGetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceOption>(sql);
        }

        public async Task UpdateAsync(MultipleChoiceOption option)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, option);
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceOptions (OptionText, MultipleChoiceQuestionId)
                VALUES (@OptionText, @MultipleChoiceQuestionId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM MultipleChoiceOptions WHERE Id = @Id";
        }

        private string GetGetAllSQL()
        {
            return "SELECT * FROM MultipleChoiceOptions";
        }

        private string GetGetByIdSQL()
        {
            return "SELECT * FROM MultipleChoiceOptions WHERE Id = @Id";
        }

        private string GetGetByQuestionIdSQL()
        {
            return "SELECT * FROM MultipleChoiceOptions WHERE MultipleChoiceQuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE MultipleChoiceOptions
                SET OptionText = @OptionText,
                    MultipleChoiceQuestionId = @MultipleChoiceQuestionId
                WHERE Id = @Id
            ";
        }
    }
}
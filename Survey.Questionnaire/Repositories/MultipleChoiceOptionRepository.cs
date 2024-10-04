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

        public async Task<int> CreateAsync(MultipleChoiceOptionRepoModel option)
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

        public async Task<IEnumerable<MultipleChoiceOptionRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceOptionRepoModel>(sql, new { QuestionId = questionId });
        }

        public async Task<MultipleChoiceOptionRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryFirstOrDefaultAsync<MultipleChoiceOptionRepoModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<MultipleChoiceOptionRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            return await connection.QueryAsync<MultipleChoiceOptionRepoModel>(sql);
        }

        public async Task UpdateAsync(MultipleChoiceOptionRepoModel option)
        {
            var sql = GetUpdateSQL();
            using var connection = _dbConnectionFactory.CreateConnection();
            await connection.ExecuteAsync(sql, option);
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceOption (OptionText, MultipleChoiceQuestionId)
                VALUES (@OptionText, @MultipleChoiceQuestionId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM MultipleChoiceOptions WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM MultipleChoiceOptions";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM MultipleChoiceOptions WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
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
using Dapper;
using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class MultipleChoiceOptionRepository : IMultipleChoiceOptionRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<MultipleChoiceOptionRepository> _logger;

        public MultipleChoiceOptionRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                              IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                              ILogger<MultipleChoiceOptionRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(MultipleChoiceOptionRepoModel option)
        {
            var sql = GetCreateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var id = await connection.QuerySingleAsync<int>(sql, option);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating multiple choice option: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, new { Id = id });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting multiple choice option with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceOptionRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<MultipleChoiceOptionRepoModel>(sql, new { QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving multiple choice options by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(MultipleChoiceOptionRepoModel option)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, option);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating multiple choice option: {Message}", ex.Message);
                throw;
            }
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceOption (OptionText, QuestionId)
                VALUES (@OptionText, @QuestionId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM MultipleChoiceOption WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM MultipleChoiceOption WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE MultipleChoiceOptions
                SET OptionText = @OptionText,
                    QuestionId = @QuestionId
                WHERE Id = @Id
            ";
        }
    }
}
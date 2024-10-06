using Dapper;
using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class TextQuestionAnswerRepository : ITextQuestionAnswerRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<TextQuestionAnswerRepository> _logger;

        public TextQuestionAnswerRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                            IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                            ILogger<TextQuestionAnswerRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(TextQuestionAnswerRepoModel answer)
        {
            var sql = GetCreateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var id = await connection.QuerySingleAsync<int>(sql, answer);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating text question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var sql = GetDeleteSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting text question answer with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<TextQuestionAnswerRepoModel>(sql, new { QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving text question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(TextQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating text question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<TextQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving text question answers by questionnaire ID {QuestionnaireId}: {Message}", questionnaireId, ex.Message);
                throw;
            }
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
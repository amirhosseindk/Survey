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
                var rowsAffected = await connection.ExecuteAsync(sql, answer);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating text question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(TextQuestionAnswerRepoModel answer)
        {
            var sql = GetDeleteSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, answer);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting text question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<TextQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnnaireId, QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving text question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(TextQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, answer);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating text question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId)
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

        public async Task<IEnumerable<TextQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId)
        {
            var sql = GetAllAnswersSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<TextQuestionAnswerRepoModel>(sql, new { QuestionnaireId = QuestionnaireId, StudentId = StudentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting multiple choice questions answer: {Message}", ex.Message);
                throw;
            }
        }

        private string GetAllAnswersSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND StudentId = @StudentId";
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO TextQuestionAnswers (QuestionnaireId, QuestionId, AnswerText, StudentId, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @AnswerText, @StudentId, @FillDateTime);";
        }

        private string GetDeleteSQL()
        {
            return @"
                DELETE FROM TextQuestionAnswers
                WHERE QuestionnaireId = @QuestionnaireId
                  AND QuestionId = @QuestionId
                  AND StudentId = @StudentId";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE TextQuestionAnswers
                SET AnswerText = @AnswerText,
                    FillDateTime = @FillDateTime
                WHERE QuestionnaireId = @QuestionnaireId
                      AND QuestionId = @QuestionId
                      AND StudentId = @StudentId";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM TextQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
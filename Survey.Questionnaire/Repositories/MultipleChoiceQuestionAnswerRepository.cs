using Dapper;
using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class MultipleChoiceQuestionAnswerRepository : IMultipleChoiceQuestionAnswerRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<MultipleChoiceQuestionAnswerRepository> _logger;

        public MultipleChoiceQuestionAnswerRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                                      IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                                      ILogger<MultipleChoiceQuestionAnswerRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(MultipleChoiceQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while creating multiple choice question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(MultipleChoiceQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while deleting multiple choice question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new {QuestionnaireId = questionnnaireId, QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving multiple choice question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(MultipleChoiceQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while updating multiple choice question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving multiple choice question answers by questionnaire ID {QuestionnaireId}: {Message}", questionnaireId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId)
        {
            var sql = GetAllAnswersSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { QuestionnaireId = QuestionnaireId, StudentId = StudentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting multiple choice questions answer: {Message}", ex.Message);
                throw;
            }
        }

        private string GetAllAnswersSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND StudentId = @StudentId";
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceQuestionAnswers (QuestionnaireId, QuestionId, AnswerOptionId, StudentId, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @AnswerOptionId, @StudentId, @FillDateTime);
            ";
        }

        private string GetDeleteSQL()
        {
            return @"
                DELETE FROM MultipleChoiceQuestionAnswers
                WHERE QuestionnaireId = @QuestionnaireId
                  AND QuestionId = @QuestionId
                  AND StudentId = @StudentId";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE MultipleChoiceQuestionAnswers
                SET AnswerOptionId = @AnswerOptionId,
                    FillDateTime = @FillDateTime
                WHERE QuestionnaireId = @QuestionnaireId
                      AND QuestionId = @QuestionId
                      AND StudentId = @StudentId";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
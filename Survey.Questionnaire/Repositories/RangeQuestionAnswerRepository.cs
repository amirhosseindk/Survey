using Dapper;
using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class RangeQuestionAnswerRepository : IRangeQuestionAnswerRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<RangeQuestionAnswerRepository> _logger;

        public RangeQuestionAnswerRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                             IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                             ILogger<RangeQuestionAnswerRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(RangeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while creating range question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(RangeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while deleting range question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnnaireId, QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving range question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(RangeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while updating range question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving range question answers by questionnaire ID {QuestionnaireId}: {Message}", questionnaireId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<RangeQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId)
        {
            var sql = GetAllAnswersSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<RangeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = QuestionnaireId, StudentId = StudentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting range question answers: {Message}", ex.Message);
                throw;
            }
        }

        private string GetAllAnswersSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND StudentId = @StudentId";
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO RangeQuestionAnswers (QuestionnaireId, QuestionId, StudentId, AnswerValue, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @StudentId, @AnswerValue, @FillDateTime);";
        }

        private string GetDeleteSQL()
        {
            return @"
                DELETE FROM RangeQuestionAnswers
                WHERE QuestionnaireId = @QuestionnaireId
                  AND QuestionId = @QuestionId
                  AND StudentId = @StudentId";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE RangeQuestionAnswers
                SET AnswerValue = @AnswerValue,
                    FillDateTime = @FillDateTime
                WHERE QuestionnaireId = @QuestionnaireId
                      AND QuestionId = @QuestionId
                      AND StudentId = @StudentId";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM RangeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
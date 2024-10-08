using Dapper;
using Microsoft.Extensions.Logging;
using Survey.Questionnaires.Contracts;
using Survey.Questionnaires.Models;

namespace Survey.Questionnaires.Repositories
{
    public class DegreeQuestionAnswerRepository : IDegreeQuestionAnswerRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<DegreeQuestionAnswerRepository> _logger;

        public DegreeQuestionAnswerRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                              IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                              ILogger<DegreeQuestionAnswerRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(DegreeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while creating degree question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(DegreeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while deleting degree question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllAnswersOfQuestionIdAsync(int questionnnaireId, int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<DegreeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnnaireId, QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving degree question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(DegreeQuestionAnswerRepoModel answer)
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
                _logger.LogError(ex, "Error while updating degree question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllOfQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<DegreeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = questionnaireId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving degree question answers by questionnaire ID {QuestionnaireId}: {Message}", questionnaireId, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetAllAnswersAsync(int QuestionnaireId, string StudentId)
        {
            var sql = GetAllAnswersSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<DegreeQuestionAnswerRepoModel>(sql, new { QuestionnaireId = QuestionnaireId, StudentId = StudentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting degree question answers: {Message}", ex.Message);
                throw;
            }
        }

        private string GetAllAnswersSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND StudentId = @StudentId";
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO DegreeQuestionAnswers (QuestionnaireId, QuestionId, StudentId, AnswerValue, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @StudentId, @AnswerValue, @FillDateTime);";
        }

        private string GetDeleteSQL()
        {
            return @"
                DELETE FROM DegreeQuestionAnswers
                WHERE QuestionnaireId = @QuestionnaireId
                  AND QuestionId = @QuestionId
                  AND StudentId = @StudentId";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId AND QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE DegreeQuestionAnswers
                SET AnswerValue = @AnswerValue,
                    FillDateTime = @FillDateTime
                WHERE QuestionnaireId = @QuestionnaireId
                      AND QuestionId = @QuestionId
                      AND StudentId = @StudentId";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM DegreeQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
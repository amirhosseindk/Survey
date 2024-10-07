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
                _logger.LogError(ex, "Error while deleting multiple choice question answer with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<MultipleChoiceQuestionAnswerRepoModel>(sql, new { QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving multiple choice question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(MultipleChoiceQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating multiple choice question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<MultipleChoiceQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
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

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO MultipleChoiceQuestionAnswers (QuestionnaireId, QuestionId, AnswerOptionId, StudentId, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @AnswerOptionId, @StudentId, @FillDateTime);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM MultipleChoiceQuestionAnswers WHERE Id = @Id";
        }

        private string GetByQuestionIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionId = @QuestionId";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE MultipleChoiceQuestionAnswers
                SET QuestionnaireId = @QuestionnaireId,
                    QuestionId = @QuestionId,
                    AnswerOptionId = @AnswerOptionId,
                    StudentId = @StudentId,
                    FillDateTime = @FillDateTime
                WHERE Id = @Id
            ";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM MultipleChoiceQuestionAnswers WHERE QuestionnaireId = @QuestionnaireId";
        }
    }
}
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
                _logger.LogError(ex, "Error while deleting degree question answer with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetByQuestionIdAsync(int questionId)
        {
            var sql = GetByQuestionIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<DegreeQuestionAnswerRepoModel>(sql, new { QuestionId = questionId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving degree question answers by question ID {QuestionId}: {Message}", questionId, ex.Message);
                throw;
            }
        }

        public async Task UpdateAsync(DegreeQuestionAnswerRepoModel answer)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                await connection.ExecuteAsync(sql, answer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating degree question answer: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<DegreeQuestionAnswerRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
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

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO DegreeQuestionAnswers (QuestionnaireId, QuestionId, StudentId, AnswerValue, FillDateTime)
                VALUES (@QuestionnaireId, @QuestionId, @StudentId, @AnswerValue, @FillDateTime);";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM DegreeQuestionAnswers WHERE Id = @Id";
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
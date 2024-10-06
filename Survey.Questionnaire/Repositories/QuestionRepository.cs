using Dapper;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Contracts;
using Microsoft.Extensions.Logging;

namespace Survey.Questionnaires.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<QuestionRepository> _logger;

        public QuestionRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                  IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                  ILogger<QuestionRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(QuestionRepoModel question)
        {
            var sql = GetCreateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var id = await connection.QuerySingleAsync<int>(sql, question);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating question: {Message}", ex.Message);
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
                _logger.LogError(ex, "Error while deleting question with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<QuestionRepoModel>> GetByQuestionnaireIdAsync(int questionnaireId)
        {
            var sql = GetByQuestionnaireIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<QuestionRepoModel>(sql, new { QuestionnaireId = questionnaireId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving questions by questionnaire ID {QuestionnaireId}: {Message}", questionnaireId, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(QuestionRepoModel question)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, question);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating question: {Message}", ex.Message);
                throw;
            }
        }

        private string GetCreateSQL()
        {
            return @"
                INSERT INTO Questions (Rank, Type, Title, QuestionnaireId)
                VALUES (@Rank, @Type, @Title, @QuestionnaireId);
                SELECT CAST(SCOPE_IDENTITY() as int);
            ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Questions WHERE Id = @Id";
        }

        private string GetByQuestionnaireIdSQL()
        {
            return "SELECT * FROM Questions WHERE QuestionnaireId = @QuestionnaireId ORDER BY Rank";
        }

        private string GetUpdateSQL()
        {
            return @"
                UPDATE Questions
                SET Rank = @Rank,
                    Type = @Type,
                    Title = @Title,
                    QuestionnaireId = @QuestionnaireId
                WHERE Id = @Id
            ";
        }
    }
}
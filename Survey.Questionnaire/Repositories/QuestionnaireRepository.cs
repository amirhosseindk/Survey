using Dapper;
using Survey.Questionnaires.Models;
using Survey.Questionnaires.Contracts;
using Microsoft.Extensions.Logging;

namespace Survey.Questionnaires.Repositories
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly IQuestionnaireReadConnectionFactory _readConnectionFactory;
        private readonly IQuestionnaireWriteConnectionFactory _writeConnectionFactory;
        private readonly ILogger<QuestionnaireRepository> _logger;

        public QuestionnaireRepository(IQuestionnaireReadConnectionFactory readConnectionFactory,
                                       IQuestionnaireWriteConnectionFactory writeConnectionFactory,
                                       ILogger<QuestionnaireRepository> logger)
        {
            _readConnectionFactory = readConnectionFactory;
            _writeConnectionFactory = writeConnectionFactory;
            _logger = logger;
        }

        public async Task<int> CreateAsync(QuestionnaireRepoModel questionnaire)
        {
            var sql = GetCreateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var id = await connection.QuerySingleAsync<int>(sql, questionnaire);
                return id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating questionnaire: {Message}", ex.Message);
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
                _logger.LogError(ex, "Error while deleting questionnaire with ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<QuestionnaireRepoModel>> GetAllAsync()
        {
            var sql = GetAllSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<QuestionnaireRepoModel>(sql);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving all questionnaires: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<QuestionnaireRepoModel>> GetAllByProfessorIdAsync(string professorId)
        {
            var sql = GetAllByProfessorIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<QuestionnaireRepoModel>(sql, new { ProfessorId = professorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving questionnaires by professor ID {ProfessorId}: {Message}", professorId, ex.Message);
                throw;
            }
        }

        public async Task<QuestionnaireRepoModel> GetByIdAsync(int id)
        {
            var sql = GetByIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<QuestionnaireRepoModel>(sql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving questionnaire by ID {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public async Task<QuestionnaireRepoModel> GetByTitleAsync(string title)
        {
            var sql = GetByTitleSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryFirstOrDefaultAsync<QuestionnaireRepoModel>(sql, new { Title = title });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving questionnaire by title {Title}: {Message}", title, ex.Message);
                throw;
            }
        }

        public async Task<bool> UpdateAsync(QuestionnaireRepoModel questionnaire)
        {
            var sql = GetUpdateSQL();
            try
            {
                using var connection = _writeConnectionFactory.CreateConnection();
                var rowsAffected = await connection.ExecuteAsync(sql, questionnaire);
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating questionnaire: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<QuestionnaireRepoModel>> GetAllByClassIdAsync(int classId)
        {
            var sql = GetAllByClassIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<QuestionnaireRepoModel>(sql, new { ClassId = classId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all questionnaires by class id: {Message}", ex.Message);
                throw;
            }
        }

        public async Task<IEnumerable<QuestionnaireRepoModel>> GetAllByStudentIdAsync(string studentId)
        {
            var sql = GetAllByStudentIdSQL();
            try
            {
                using var connection = _readConnectionFactory.CreateConnection();
                return await connection.QueryAsync<QuestionnaireRepoModel>(sql, new { StudentId = studentId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting all questionnaires by student id: {Message}", ex.Message);
                throw;
            }
        }

        private string GetAllByClassIdSQL()
        {
            return "SELECT * FROM Questionnaires WHERE ClassId = @ClassId";
        }

        private string GetAllByStudentIdSQL()
        {
            return @"SELECT q.* 
                FROM Questionnaires q 
                INNER JOIN Classes c ON q.ClassId = c.Id 
                INNER JOIN ClassStudents cs ON c.Id = cs.ClassId
                WHERE cs.StudentId = @StudentId";
        }

        private string GetCreateSQL()
        {
            return @"
            INSERT INTO Questionnaires (Title, ClassId, ProfessorId)
            VALUES (@Title, @ClassId, @ProfessorId);
            SELECT CAST(SCOPE_IDENTITY() as int);
        ";
        }

        private string GetDeleteSQL()
        {
            return "DELETE FROM Questionnaires WHERE Id = @Id";
        }

        private string GetAllSQL()
        {
            return "SELECT * FROM Questionnaires";
        }

        private string GetAllByProfessorIdSQL()
        {
            return "SELECT * FROM Questionnaires WHERE ProfessorId = @ProfessorId";
        }

        private string GetByIdSQL()
        {
            return "SELECT * FROM Questionnaires WHERE Id = @Id";
        }

        private string GetByTitleSQL()
        {
            return "SELECT * FROM Questionnaires WHERE Title = @Title";
        }

        private string GetUpdateSQL()
        {
            return @"
            UPDATE Questionnaires
            SET Title = @Title,
                ClassId = @ClassId,
                ProfessorId = @ProfessorId
            WHERE Id = @Id
        ";
        }
    }
}
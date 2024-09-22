using Microsoft.Extensions.Logging;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IClassStudentsRepository _classStudentsRepository;
        private readonly ILogger<UniversityService> _logger;


        public UniversityService(
            IClassRepository classRepository,
            ICourseRepository courseRepository,
            IClassStudentsRepository classStudentsRepository,
            ILogger<UniversityService> logger)
        {
            _classRepository = classRepository;
            _courseRepository = courseRepository;
            _classStudentsRepository = classStudentsRepository;
            _logger = logger;
        }

        public async Task<int> CreateCourseAsync(Course course)
        {
            try
            {
                return await _courseRepository.CreateAsync(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating course - {ex.Message}");
                throw;
            }
        }

        public async Task UpdateCourseAsync(Course course)
        {
            try
            {
                await _courseRepository.UpdateAsync(course);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating course - {ex.Message}");
                throw;
            }
        }

        public async Task DeleteCourseAsync(int id)
        {
            try
            {
                await _courseRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting course - {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            try
            {
                return await _courseRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting course - {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseByNameAsync(string name)
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                return courses.FirstOrDefault(c => c.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting course - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                return await _courseRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting courses - {ex.Message}");
                throw;
            }
        }

        public async Task<int> CreateClassAsync(Class @class)
        {
            try
            {
                return await _classRepository.CreateAsync(@class);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating class - {ex.Message}");
                throw;
            }
        }

        public async Task UpdateClassAsync(Class @class)
        {
            try
            {
                await _classRepository.UpdateAsync(@class);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating class - {ex.Message}");
                throw;
            }
        }

        public async Task DeleteClassAsync(int id)
        {
            try
            {
                await _classRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting class - {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            try
            {
                return await _classRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting class - {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassByNameAsync(string name)
        {
            try
            {
                var classes = await _classRepository.GetAllAsync();
                return classes.FirstOrDefault(c => c.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting class - {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            try
            {
                return await _classRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting classes - {ex.Message}");
                throw;
            }
        }

        public async Task AddStudentToClass(int classId, string studentId)
        {
            try
            {
                var classStudent = new ClassStudents
                {
                    ClassesId = classId,
                    StudentsId = studentId
                };
                await _classStudentsRepository.CreateAsync(classStudent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding student to class - {ex.Message}");
                throw;
            }
        }

        public async Task RemoveStudentToClass(int classId, string studentId)
        {
            try
            {
                await _classStudentsRepository.DeleteAsync(classId, studentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing student from class - {ex.Message}");
                throw;
            }
        }
    }
}
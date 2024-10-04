using Microsoft.Extensions.Logging;
using Survey.University.Contracts;
using Survey.University.Models;

namespace Survey.University.Services
{
    public class UniversityService : IUniversityService
    {
        private readonly IClassRepository _classRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogger<UniversityService> _logger;


        public UniversityService(
            IClassRepository classRepository,
            ICourseRepository courseRepository,
            ILogger<UniversityService> logger)
        {
            _classRepository = classRepository;
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<int> CreateCourseAsync(Course course)
        {
            try
            {
                return await _courseRepository.CreateAsync(new CourseRepoModel
                {
                    Name = course.Name,
                    ProfessorId = course.ProfessorId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating course - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            try
            {
                return await _courseRepository.UpdateAsync(new CourseRepoModel
                {
                    Id = course.Id,
                    Name = course.Name,
                    ProfessorId = course.ProfessorId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating course - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                return await _courseRepository.DeleteAsync(id);
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
                var course = await _courseRepository.GetByIdAsync(id);
                return new Course { Id = course.Id, Name = course.Name, ProfessorId = course.ProfessorId };
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
                var course = courses.FirstOrDefault(c => c.Name == name);
                return new Course { Id = course.Id, Name = course.Name, ProfessorId= course.ProfessorId };
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
                var courses = await _courseRepository.GetAllAsync();

                var result = new List<Course>();

                foreach (var course in courses)
                {
                    result.Add(new Course
                    {
                        Id = course.Id,
                        Name = course.Name,
                        ProfessorId = course.ProfessorId
                    });
                }

                return result;
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
                return await _classRepository.CreateAsync(new ClassRepoModel
                {
                    CourseId = @class.Course.Id,
                    Name = @class.Name,
                    Id = @class.Id,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating class - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateClassAsync(Class @class)
        {
            try
            {
                return await _classRepository.UpdateAsync(new ClassRepoModel
                {
                    Id = @class.Id,
                    CourseId = @class.Course.Id,
                    Name= @class.Name
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating class - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            try
            {
                return await _classRepository.DeleteAsync(id);
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
                var @class = await _classRepository.GetByIdAsync(id);
                return new Class { Id = @class.Id, Name = @class.Name, Course = await GetCourseByIdAsync(@class.CourseId) };
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
                var @class = classes.FirstOrDefault(c => c.Name == name);
                return new Class { Id = @class.Id, Name = @class.Name, Course = await GetCourseByIdAsync(@class.CourseId) };
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
                var classes = await _classRepository.GetAllAsync();
                var result = new List<Class>();

                foreach (var @class in classes)
                {
                    result.Add(new Class
                    {
                        Id = @class.Id,
                        Name = @class.Name,
                        Course = await GetCourseByIdAsync(@class.CourseId)
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while getting classes - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> AddStudentToClassAsync(int classId, string studentId)
        {
            try
            {
                var classStudent = new ClassStudentRepoModel
                {
                    ClassId = classId,
                    StudentId = studentId
                };
                return await _classRepository.AddStudentToClassAsync(classStudent) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while adding student to class - {ex.Message}");
                throw;
            }
        }

        public async Task<bool> RemoveStudentFromClassAsync(int classId, string studentId)
        {
            try
            {
                return await _classRepository.RemoveStudentFromClassAsync(classId, studentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while removing student from class - {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId)
        {
            try
            {
                return await _courseRepository.GetClassesByCourseIdAsync(courseId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving classes for course ID {courseId} - {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName)
        {
            try
            {
                return await _courseRepository.GetClassesByCourseNameAsync(courseName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving classes for course name {courseName} - {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassIdAsync(int classId)
        {
            try
            {
                return await _classRepository.GetStudentsByClassIdAsync(classId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving students for class ID {classId} - {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassNameAsync(string className)
        {
            try
            {
                return await _classRepository.GetStudentsByClassNameAsync(className);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving students for class name {className} - {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseWithClassesByIdAsync(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                var classes = await _courseRepository.GetClassesByCourseIdAsync(courseId);

                return new Course
                {
                    Id = course.Id,
                    Name = course.Name,
                    ProfessorId = course.ProfessorId,
                    Classes = classes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving course with classes for course ID {courseId} - {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseWithClassesByNameAsync(string courseName)
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                var course = courses.FirstOrDefault(c => c.Name == courseName);
                var classes = await _courseRepository.GetClassesByCourseNameAsync(courseName);

                return new Course
                {
                    Id = course.Id,
                    Name = course.Name,
                    ProfessorId = course.ProfessorId,
                    Classes = classes
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving course with classes for course name {courseName} - {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassWithStudentsByIdAsync(int classId)
        {
            try
            {
                var classRepoModel = await _classRepository.GetByIdAsync(classId);
                var students = await _classRepository.GetStudentsByClassIdAsync(classId);

                return new Class
                {
                    Id = classRepoModel.Id,
                    Name = classRepoModel.Name,
                    Course = await GetCourseByIdAsync(classRepoModel.CourseId),
                    Students = students
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving class with students for class ID {classId} - {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassWithStudentsByNameAsync(string className)
        {
            try
            {
                var classes = await _classRepository.GetAllAsync();
                var classRepoModel = classes.FirstOrDefault(c => c.Name == className);
                var students = await _classRepository.GetStudentsByClassNameAsync(className);

                return new Class
                {
                    Id = classRepoModel.Id,
                    Name = classRepoModel.Name,
                    Course = new Course { Id = classRepoModel.CourseId },
                    Students = students
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving class with students for class name {className} - {ex.Message}");
                throw;
            }
        }
    }
}
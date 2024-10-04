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
                if (course == null || string.IsNullOrWhiteSpace(course.Name))
                {
                    _logger.LogError("Invalid course data provided");
                    throw new ArgumentException("Invalid course data");
                }

                return await _courseRepository.CreateAsync(new CourseRepoModel
                {
                    Name = course.Name.ToLower(),
                    ProfessorId = course.ProfessorId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating course: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateCourseAsync(Course course)
        {
            try
            {
                if (course == null || string.IsNullOrWhiteSpace(course.Name))
                {
                    _logger.LogError("Invalid course data provided for update");
                    throw new ArgumentException("Invalid course data");
                }

                return await _courseRepository.UpdateAsync(new CourseRepoModel
                {
                    Id = course.Id,
                    Name = course.Name.ToLower(),
                    ProfessorId = course.ProfessorId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating course: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            try
            {
                var result = await _courseRepository.DeleteAsync(id);
                if (!result)
                {
                    throw new KeyNotFoundException("Course not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting course: {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(id);
                if (course == null) throw new KeyNotFoundException("Course not found");

                return new Course { Id = course.Id, Name = course.Name, ProfessorId = course.ProfessorId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving course by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseByNameAsync(string name)
        {
            try
            {
                var course = await _courseRepository.GetByNameAsync(name.ToLower());
                if (course == null) throw new KeyNotFoundException("Course not found");

                return new Course { Id = course.Id, Name = course.Name, ProfessorId = course.ProfessorId };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving course by name: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Course>> GetAllCoursesAsync()
        {
            try
            {
                var courses = await _courseRepository.GetAllAsync();
                if (courses == null) throw new KeyNotFoundException("No courses found");

                return courses.Select(c => new Course { Id = c.Id, Name = c.Name, ProfessorId = c.ProfessorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving all courses: {ex.Message}");
                throw;
            }
        }

        public async Task<int> CreateClassAsync(Class @class)
        {
            try
            {
                if (@class == null || string.IsNullOrWhiteSpace(@class.Name))
                {
                    _logger.LogError("Invalid class data provided");
                    throw new ArgumentException("Invalid class data");
                }

                return await _classRepository.CreateAsync(new ClassRepoModel
                {
                    CourseId = @class.Course.Id,
                    Name = @class.Name.ToLower(),
                    Id = @class.Id,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating class: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> UpdateClassAsync(Class @class)
        {
            try
            {
                if (@class == null || string.IsNullOrWhiteSpace(@class.Name))
                {
                    _logger.LogError("Invalid class data provided for update");
                    throw new ArgumentException("Invalid class data");
                }

                return await _classRepository.UpdateAsync(new ClassRepoModel
                {
                    Id = @class.Id,
                    CourseId = @class.Course.Id,
                    Name = @class.Name.ToLower()
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating class: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            try
            {
                var result = await _classRepository.DeleteAsync(id);
                if (!result)
                {
                    throw new KeyNotFoundException("Class not found");
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting class: {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassByIdAsync(int id)
        {
            try
            {
                var @class = await _classRepository.GetByIdAsync(id);
                if (@class == null) throw new KeyNotFoundException("Class not found");

                return new Class { Id = @class.Id, Name = @class.Name, Course = await GetCourseByIdAsync(@class.CourseId) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving class by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassByNameAsync(string name)
        {
            try
            {
                var @class = await _classRepository.GetByNameAsync(name.ToLower());
                if (@class == null) throw new KeyNotFoundException("Class not found");

                return new Class { Id = @class.Id, Name = @class.Name, Course = await GetCourseByIdAsync(@class.CourseId) };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving class by name: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<Class>> GetAllClassesAsync()
        {
            try
            {
                var classes = await _classRepository.GetAllAsync();
                if (classes == null) throw new KeyNotFoundException("No classes found");

                return classes.Select(c => new Class
                {
                    Id = c.Id,
                    Name = c.Name,
                    Course = GetCourseByIdAsync(c.CourseId).Result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving all classes: {ex.Message}");
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
                _logger.LogError(ex, $"Error while adding student to class: {ex.Message}");
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
                _logger.LogError(ex, $"Error while removing student from class: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId)
        {
            try
            {
                var result = await _courseRepository.GetClassesByCourseIdAsync(courseId);
                return result ?? throw new KeyNotFoundException("No classes found for this course ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving classes for course ID {courseId}: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName)
        {
            try
            {
                var result = await _courseRepository.GetClassesByCourseNameAsync(courseName.ToLower());
                return result ?? throw new KeyNotFoundException("No classes found for this course name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving classes for course name {courseName}: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassIdAsync(int classId)
        {
            try
            {
                var result = await _classRepository.GetStudentsByClassIdAsync(classId);
                return result ?? throw new KeyNotFoundException("No students found for this class ID");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving students for class ID {classId}: {ex.Message}");
                throw;
            }
        }

        public async Task<Dictionary<string, string>> GetStudentsByClassNameAsync(string className)
        {
            try
            {
                var result = await _classRepository.GetStudentsByClassNameAsync(className.ToLower());
                return result ?? throw new KeyNotFoundException("No students found for this class name");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving students for class name {className}: {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseWithClassesByIdAsync(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null) throw new KeyNotFoundException("Course not found");

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
                _logger.LogError(ex, $"Error while retrieving course with classes for course ID {courseId}: {ex.Message}");
                throw;
            }
        }

        public async Task<Course> GetCourseWithClassesByNameAsync(string courseName)
        {
            try
            {
                var course = await _courseRepository.GetByNameAsync(courseName.ToLower());
                if (course == null) throw new KeyNotFoundException("Course not found");

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
                _logger.LogError(ex, $"Error while retrieving course with classes for course name {courseName}: {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassWithStudentsByIdAsync(int classId)
        {
            try
            {
                var classRepoModel = await _classRepository.GetByIdAsync(classId);
                if (classRepoModel == null) throw new KeyNotFoundException("Class not found");

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
                _logger.LogError(ex, $"Error while retrieving class with students for class ID {classId}: {ex.Message}");
                throw;
            }
        }

        public async Task<Class> GetClassWithStudentsByNameAsync(string className)
        {
            try
            {
                var @class = await _classRepository.GetByNameAsync(className.ToLower());
                if (@class == null) throw new KeyNotFoundException("Class not found");

                var students = await _classRepository.GetStudentsByClassNameAsync(className);
                return new Class
                {
                    Id = @class.Id,
                    Name = @class.Name,
                    Course = await GetCourseByIdAsync(@class.CourseId),
                    Students = students
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while retrieving class with students for class name {className}: {ex.Message}");
                throw;
            }
        }
    }
}
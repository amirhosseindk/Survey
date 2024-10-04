using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface IUniversityService
    {
        Task<int> CreateCourseAsync(Course course);
        Task<bool> UpdateCourseAsync(Course course);
        Task<bool> DeleteCourseAsync(int id);
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> GetCourseByNameAsync(string name);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<int> CreateClassAsync(Class @class);
        Task<bool> UpdateClassAsync(Class @class);
        Task<bool> DeleteClassAsync(int id);
        Task<Class> GetClassByIdAsync(int id);
        Task<Class> GetClassByNameAsync(string name);
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task<bool> AddStudentToClassAsync(int classId, string studentId);
        Task<bool> RemoveStudentFromClassAsync(int classId, string studentId);
        Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId);
        Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName);
        Task<Dictionary<string, string>> GetStudentsByClassIdAsync(int classId);
        Task<Dictionary<string, string>> GetStudentsByClassNameAsync(string className);
        Task<Course> GetCourseWithClassesByIdAsync(int courseId);
        Task<Course> GetCourseWithClassesByNameAsync(string courseName);
        Task<Class> GetClassWithStudentsByIdAsync(int classId);
        Task<Class> GetClassWithStudentsByNameAsync(string className);
    }
}
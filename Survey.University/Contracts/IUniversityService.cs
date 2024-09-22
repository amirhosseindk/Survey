using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface IUniversityService
    {
        Task<int> CreateCourseAsync(Course course);
        Task UpdateCourseAsync(Course course);
        Task DeleteCourseAsync(int id);
        Task<Course> GetCourseByIdAsync(int id);
        Task<Course> GetCourseByNameAsync(string name);
        Task<IEnumerable<Course>> GetAllCoursesAsync();
        Task<int> CreateClassAsync(Class @class);
        Task UpdateClassAsync(Class @class);
        Task DeleteClassAsync(int id);
        Task<Class> GetClassByIdAsync(int id);
        Task<Class> GetClassByNameAsync(string name);
        Task<IEnumerable<Class>> GetAllClassesAsync();
        Task AddStudentToClass(int classId, string studentId);
        Task RemoveStudentToClass(int classId, string studentId);
    }
}
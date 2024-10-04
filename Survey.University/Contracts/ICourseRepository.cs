using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface ICourseRepository
    {
        Task<int> CreateAsync(CourseRepoModel course);
        Task<CourseRepoModel> GetByIdAsync(int id);
        Task<IEnumerable<CourseRepoModel>> GetAllAsync();
        Task<bool> UpdateAsync(CourseRepoModel course);
        Task<bool> DeleteAsync(int id);
        Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId);
        Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName);
    }
}
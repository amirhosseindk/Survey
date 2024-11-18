using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface ICourseRepository
    {
        Task<int> CreateAsync(CourseRepoModel course);
        Task<CourseRepoModel> GetByIdAsync(int id);
        Task<CourseRepoModel> GetByNameAsync(string name);
        Task<IEnumerable<CourseRepoModel>> GetAllAsync();
        Task<IEnumerable<CourseRepoModel>> GetAllByProfessorIdAsync(string professorId);
        Task<bool> UpdateAsync(CourseRepoModel course);
        Task<bool> DeleteAsync(int id);
        Task<Dictionary<int, string>> GetClassesByCourseIdAsync(int courseId);
        Task<Dictionary<int, string>> GetClassesByCourseNameAsync(string courseName);
        Task<CourseRepoModel> GetByClassIdAsync(int id);
    }
}
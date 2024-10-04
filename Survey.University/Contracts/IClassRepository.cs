using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface IClassRepository
    {
        Task<int> CreateAsync(ClassRepoModel @class);
        Task<ClassRepoModel> GetByIdAsync(int id);
        Task<ClassRepoModel> GetByNameAsync(string name);
        Task<IEnumerable<ClassRepoModel>> GetAllAsync();
        Task<bool> UpdateAsync(ClassRepoModel @class);
        Task<bool> DeleteAsync(int id);
        Task<int> AddStudentToClassAsync(ClassStudentRepoModel classStudents);
        Task<bool> RemoveStudentFromClassAsync(int classId, string studentId);
        Task<Dictionary<string, string>> GetStudentsByClassIdAsync(int classId);
        Task<Dictionary<string, string>> GetStudentsByClassNameAsync(string className);
    }
}
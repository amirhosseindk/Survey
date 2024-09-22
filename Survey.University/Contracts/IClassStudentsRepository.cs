using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface IClassStudentsRepository
    {
        Task<int> CreateAsync(ClassStudents classStudents);
        Task<ClassStudents> GetByIdAsync(int classId, int studentId);
        Task<IEnumerable<ClassStudents>> GetAllAsync();
        Task UpdateAsync(ClassStudents classStudents);
        Task DeleteAsync(int classId, string studentId);
    }
}
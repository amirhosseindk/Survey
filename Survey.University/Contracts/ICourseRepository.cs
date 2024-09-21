using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface ICourseRepository
    {
        Task<int> CreateAsync(Course course);
        Task<Course> GetByIdAsync(int id);
        Task<IEnumerable<Course>> GetAllAsync();
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}
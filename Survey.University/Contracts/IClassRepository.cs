using Survey.University.Models;

namespace Survey.University.Contracts
{
    public interface IClassRepository
    {
        Task<int> CreateAsync(Class @class);
        Task<Class> GetByIdAsync(int id);
        Task<IEnumerable<Class>> GetAllAsync();
        Task UpdateAsync(Class @class);
        Task DeleteAsync(int id);
    }
}
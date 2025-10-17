
using System.Collections.Generic;
using System.Threading.Tasks;
namespace workspace.Models.Repository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<T> GetByNameAsync(string name);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}

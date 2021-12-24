using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Application.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(long id);
        Task<bool> CreateAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        Task<bool> IsExists(long id);
    }
}

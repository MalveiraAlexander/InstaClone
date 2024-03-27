
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<int> CountAsync();
        Task<IEnumerable<TEntity>> GetByFiltersAsync(Func<TEntity, bool>? exp = null);
        Task<TEntity?> GetByIdAsync(int id);
        Task<bool> ExistByIdAsync(int id);
        Task<bool> ExistByIdAsync(Guid id);
        Task<TEntity?> CreateAsync(TEntity entity);
        Task<TEntity?> UpdateAsync(TEntity entity);
        Task<TEntity?> DeleteAsync(int id);
    }
}

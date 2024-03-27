using InstaClone.Commons.Filters;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User post, CancellationToken cancellationToken);
        Task<User> UpdateAsync(UserRequest request, int id, CancellationToken cancellationToken);
        Task<User> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User?> GetByNickNameAsync(string nickName, CancellationToken cancellationToken);
        Task<(ICollection<User> data, long count)> GetByFiltersAsync(UserFilter filter, CancellationToken cancellationToken);
    }
}

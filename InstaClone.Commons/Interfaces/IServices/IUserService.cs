using InstaClone.Commons.Filters;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IServices
{
    public interface IUserService
    {
        Task<UserResponse> CreateAsync(UserRequest request, CancellationToken cancellationToken);
        Task<UserResponse> UpdateAsync(UserRequest request, int id, CancellationToken cancellationToken);
        Task<UserResponse> DeleteAsync(int id, CancellationToken cancellationToken);
        Task<UserResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<Response<UserResponse>> GetAllAsync(UserFilter filter, CancellationToken cancellationToken);
        Task<bool> AddFollow(int userId, int followerId, CancellationToken cancellationToken);
        Task<bool> RemoveFollow(int userId, int followerId, CancellationToken cancellationToken);
    }
}

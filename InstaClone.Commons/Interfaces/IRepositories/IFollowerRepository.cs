using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IRepositories
{
    public interface IFollowerRepository
    {
        Task<bool> AddFollower(int userId, int followerId, CancellationToken cancellationToken);
        Task<bool> RemoveFollower(int userId, int followerId, CancellationToken cancellationToken);
        Task<List<User>> GetFollowersAsync(int userId, CancellationToken cancellationToken);
    }
}

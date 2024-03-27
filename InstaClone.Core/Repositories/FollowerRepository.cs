using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Data;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Repositories
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly SystemContext context;

        public FollowerRepository(SystemContext context)
        {
            this.context = context;
        }

        public async Task<bool> AddFollower(int userId, int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var follower = new UserUser
            {
                UserId = userId,
                FollowerId = followerId
            };

            await context.UserUsers.AddAsync(follower);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<bool> RemoveFollower(int userId, int followerId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var follower = await context.UserUsers.FindAsync(userId, followerId);
            if (follower is null)
            {
                throw new NotFoundException();
            }
            context.UserUsers.Remove(follower);
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public async Task<List<User>> GetFollowersAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return context.UserUsers.Where(p => p.FollowerId == userId).Select(p => p.User).ToList();
        }
    }
}

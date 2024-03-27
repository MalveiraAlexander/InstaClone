using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Data;
using InstaClone.Commons.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Repositories
{
    public class PostReactionRepository : IPostReactionRepository
    {
        private readonly SystemContext context;

        public PostReactionRepository(SystemContext context)
        {
            this.context = context;
        }

        public async Task<PostReaction> AddReaction(PostReaction postReaction, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = context.PostReactions.Add(postReaction).Entity;
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<PostReaction> DeleteReaction(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(userId, cancellationToken);
            context.PostReactions.Remove(result);
            await context.SaveChangesAsync();
            return result;
        }

        public async Task<PostReaction> GetByIdAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.PostReactions.FirstOrDefaultAsync(p => p.UserId == userId, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<PostReaction?> GetByReactionType(int userId, int reactionTypeId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.PostReactions.FirstOrDefaultAsync(p => p.UserId == userId 
                                                                           && p.ReactionTypeId == reactionTypeId, cancellationToken);
            return result;
        }
    }
}

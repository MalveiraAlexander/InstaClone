using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IRepositories
{
    public interface IPostReactionRepository
    {
        Task<PostReaction> AddReaction(PostReaction postReaction, CancellationToken cancellationToken);
        Task<PostReaction> DeleteReaction(int userId, CancellationToken cancellationToken);
        Task<PostReaction> GetByIdAsync(int userId, CancellationToken cancellationToken);
        Task<PostReaction?> GetByReactionType(int userId, int reactionTypeId, CancellationToken cancellationToken);
    }
}

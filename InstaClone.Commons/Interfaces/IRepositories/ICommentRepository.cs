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
    public interface ICommentRepository
    {
        Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken);
        Task<Comment> UpdateAsync(CommentRequest request, Guid id, CancellationToken cancellationToken);
        Task<Comment> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<(ICollection<Comment> data, long count)> GetByFiltersAsync(CommentFilter filter, CancellationToken cancellationToken);
    }
}

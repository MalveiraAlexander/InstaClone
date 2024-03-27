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
    public interface IPostRepository
    {
        Task<Post> CreateAsync(Post post, CancellationToken cancellationToken);
        Task<Post> UpdateAsync(PostRequest request, Guid id, CancellationToken cancellationToken);
        Task<Post> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<Post> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<(ICollection<Post> data, long count)> GetByFiltersAsync(PostFilter filter, CancellationToken cancellationToken);
    }
}

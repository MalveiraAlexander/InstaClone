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
    public interface IPostService
    {
        Task<PostResponse> CreateAsync(PostRequest request, CancellationToken cancellationToken);
        Task<PostResponse> UpdateAsync(PostRequest request, Guid id, CancellationToken cancellationToken);
        Task<PostResponse> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<PostResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Response<PostResponse>> GetAllAsync(PostFilter filter, CancellationToken cancellationToken);
        Task<bool> ReactAsync(PostReactionRequest request, CancellationToken cancellationToken);
        Task<bool> UnReactAsync(int userId, CancellationToken cancellationToken);
    }
}

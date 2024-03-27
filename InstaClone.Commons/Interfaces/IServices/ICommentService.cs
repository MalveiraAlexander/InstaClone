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
    public interface ICommentService
    {
        Task<CommentResponse> CreateAsync(CommentRequest request, CancellationToken cancellationToken);
        Task<CommentResponse> UpdateAsync(CommentRequest request, Guid id, CancellationToken cancellationToken);
        Task<CommentResponse> DeleteAsync(Guid id, CancellationToken cancellationToken);
        Task<CommentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Response<CommentResponse>> GetAllAsync(CommentFilter filter, CancellationToken cancellationToken);
    }
}

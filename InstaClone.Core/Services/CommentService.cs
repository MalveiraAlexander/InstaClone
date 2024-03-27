using AutoMapper;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Responses;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository repository;
        private readonly IMapper mapper;

        public CommentService(ICommentRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<CommentResponse> CreateAsync(CommentRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<CommentResponse>(await repository.CreateAsync(mapper.Map<Comment>(request), cancellationToken));
        }

        public async Task<CommentResponse> UpdateAsync(CommentRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<CommentResponse>(await repository.UpdateAsync(request, id, cancellationToken));
        }

        public async Task<CommentResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<CommentResponse>(await repository.DeleteAsync(id, cancellationToken));
        }

        public async Task<CommentResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<CommentResponse>(await repository.GetByIdAsync(id, cancellationToken));
        }

        public async Task<Response<CommentResponse>> GetAllAsync(CommentFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await repository.GetByFiltersAsync(filter, cancellationToken);
            return new Response<CommentResponse> { Data = mapper.Map<List<CommentResponse>>(result.data), Count = result.count };
        }
    }
}

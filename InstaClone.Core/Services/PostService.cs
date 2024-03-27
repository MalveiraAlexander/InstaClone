using AutoMapper;
using InstaClone.Commons.Exceptions;
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
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;
        private readonly IPostReactionRepository postReactionRepository;
        private readonly IMapper mapper;

        public PostService(IPostRepository repository,
                           IPostReactionRepository postReactionRepository,
                           IMapper mapper)
        {
            this.repository = repository;
            this.postReactionRepository = postReactionRepository;
            this.mapper = mapper;
        }

        public async Task<PostResponse> CreateAsync(PostRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<PostResponse>(await repository.CreateAsync(mapper.Map<Post>(request), cancellationToken));
        }

        public async Task<PostResponse> UpdateAsync(PostRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<PostResponse>(await repository.UpdateAsync(request, id, cancellationToken));
        }

        public async Task<PostResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<PostResponse>(await repository.DeleteAsync(id, cancellationToken));
        }

        public async Task<PostResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await repository.GetByIdAsync(id, cancellationToken);
            var response = mapper.Map<PostResponse>(result);
            response.LikeCount = result.Reactions.Count;
            return response;
        }

        public async Task<Response<PostResponse>> GetAllAsync(PostFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await repository.GetByFiltersAsync(filter, cancellationToken);
            List<PostResponse> responses = [];
            Parallel.ForEach(result.data, new ParallelOptions { MaxDegreeOfParallelism = 2 }, post =>
            {
                PostResponse response = mapper.Map<PostResponse>(post);
                response.LikeCount = post.Reactions.Count;
                responses.Add(response);
            });
            return new Response<PostResponse> { Data = responses, Count = result.count };
        }

        public async Task<bool> ReactAsync(PostReactionRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var exist = await postReactionRepository.GetByReactionType(request.UserId, request.ReactionTypeId, cancellationToken);
            if (exist is not null)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "You already reacted to this post");
            }
            var result = await postReactionRepository.AddReaction(mapper.Map<PostReaction>(request), cancellationToken);
            return true;
        }

        public async Task<bool> UnReactAsync(int userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var exist = await postReactionRepository.GetByIdAsync(userId, cancellationToken);
            if (exist is null)
            {
                throw new CustomException(System.Net.HttpStatusCode.BadRequest, "You didn't react to this post");
            }
            await postReactionRepository.DeleteReaction(userId, cancellationToken);
            return true;
        }
    }
}

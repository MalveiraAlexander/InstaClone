using AutoMapper;
using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Helpers;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Requests;
using InstaClone.Commons.Data;
using InstaClone.Commons.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaClone.Commons.Interfaces.IRepositories;

namespace InstaClone.Core.Repositories
{
    public class PostRepository : IPostRepository
    {

        private readonly SystemContext context;
        private readonly IMapper mapper;

        public PostRepository(SystemContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Post> CreateAsync(Post post, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            post.CreationDate = DateTime.UtcNow;
            var result = context.Posts.Add(post).Entity;
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<Post> UpdateAsync(PostRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            mapper.Map(request, result);
            context.Posts.Update(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<Post> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            context.Posts.Remove(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<Post> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.Posts.Include(p => p.User)
                                            .Include(p => p.Reactions)
                                            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<(ICollection<Post> data, long count)> GetByFiltersAsync(PostFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var query = await GetQueryableAsync(filter, cancellationToken);
            long count = await query.CountAsync(cancellationToken);
            if (filter.PageSize.HasValue)
            {
                query = query.Paginate(filter);
            }
            var result = await query.ToListAsync(cancellationToken);
            return (result, count);
        }


        private async Task<IQueryable<Post>> GetQueryableAsync(PostFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = context.Posts.Include(p => p.User)
                                     .Include(p => p.Reactions).AsQueryable();

            if (filter.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == filter.UserId);
            }

            if (filter.FromCreationDate.HasValue)
            {
                query = query.Where(p => p.CreationDate >= filter.FromCreationDate.Value);
            }

            if (filter.ToCreationDate.HasValue)
            {
                query = query.Where(p => p.CreationDate <= filter.ToCreationDate.Value);
            }

            return query;
        }
    }
}

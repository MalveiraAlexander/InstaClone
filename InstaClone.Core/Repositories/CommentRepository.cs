using AutoMapper;
using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Helpers;
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
    public class CommentRepository : ICommentRepository
    {
        private readonly SystemContext context;
        private readonly IMapper mapper;

        public CommentRepository(SystemContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            comment.CreationDate = DateTime.UtcNow;
            var result = context.Comments.Add(comment).Entity;
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<Comment> UpdateAsync(CommentRequest request, Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            mapper.Map(request, result);
            context.Comments.Update(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<Comment> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            context.Comments.Remove(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.Comments.Include(p => p.User)
                                               .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<(ICollection<Comment> data, long count)> GetByFiltersAsync(CommentFilter filter, CancellationToken cancellationToken)
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


        private async Task<IQueryable<Comment>> GetQueryableAsync(CommentFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = context.Comments.Include(p => p.User).AsQueryable();

            if (filter.UserId.HasValue)
            {
                query = query.Where(p => p.UserId == filter.UserId);
            }

            if (filter.PostId.HasValue)
            {
                query = query.Where(p => p.PostId == filter.PostId);
            }

            return query;
        }



    }
}

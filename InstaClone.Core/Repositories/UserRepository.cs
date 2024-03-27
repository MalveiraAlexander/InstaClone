using AutoMapper;
using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Filters;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Helpers;
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
    public class UserRepository : IUserRepository
    {
        private readonly SystemContext context;
        private readonly IMapper mapper;

        public UserRepository(SystemContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<User> CreateAsync(User post, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            post.CreationDate = DateTime.UtcNow;
            var result = context.Users.Add(post).Entity;
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }

        public async Task<User> UpdateAsync(UserRequest request, int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            mapper.Map(request, result);
            context.Users.Update(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<User> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await GetByIdAsync(id, cancellationToken);
            context.Users.Remove(result);
            await context.SaveChangesAsync(cancellationToken);
            return result;
        }


        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.Users.Include(p => p.Posts)
                                            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException();
            }
            return result;
        }

        public async Task<User?> GetByNickNameAsync(string nickName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.Users.FirstOrDefaultAsync(p => p.NickName == nickName, cancellationToken);
            return result;
        }

        public async Task<(ICollection<User> data, long count)> GetByFiltersAsync(UserFilter filter, CancellationToken cancellationToken)
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


        private async Task<IQueryable<User>> GetQueryableAsync(UserFilter filter, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var query = context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(filter.NickName))
            {
                query = query.Where(p => p.NickName.Contains(filter.NickName));
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

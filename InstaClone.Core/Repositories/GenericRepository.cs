using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using InstaClone.Commons.Data;
using InstaClone.Commons.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly SystemContext dbContext;
        private readonly IMapper mapper;
        protected readonly DbSet<TEntity> dbSet;

        public GenericRepository(SystemContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public async Task<int> CountAsync()
        {
            return dbContext.Set<TEntity>().ToList().Count();
        }

        public async Task<IEnumerable<TEntity>> GetByFiltersAsync(Func<TEntity, bool>? exp = null)
        {
            if (exp == null) { return dbContext.Set<TEntity>().ToList(); }
            return dbContext.Set<TEntity>().Where(exp);
        }

        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> ExistByIdAsync(int id)
        {
            var result = await dbContext.Set<TEntity>().FindAsync(id);
            return result != null;
        }

        public async Task<bool> ExistByIdAsync(Guid id)
        {
            var result = await dbContext.Set<TEntity>().FindAsync(id);
            return result != null;
        }

        public async Task<TEntity?> CreateAsync(TEntity entity)
        {
            var result = await dbContext.Set<TEntity>().AddAsync(entity);
            await dbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            var result = dbContext.Entry(entity).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity?> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            dbContext.Set<TEntity>().Remove(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }
    }
}

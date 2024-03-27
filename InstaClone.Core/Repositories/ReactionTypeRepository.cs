using InstaClone.Commons.Data;
using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using InstaClone.Commons.Exceptions;
using InstaClone.Commons.Interfaces.IRepositories;

namespace InstaClone.Core.Repositories
{
    public class ReactionTypeRepository : IReactionTypeRepository
    {
        private readonly SystemContext context;

        public ReactionTypeRepository(SystemContext context) 
        {
            this.context = context;
        }

        public async Task<ICollection<ReactionType>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await context.ReactionTypes.ToListAsync(cancellationToken);
        }

        public async Task<ReactionType> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var result = await context.ReactionTypes.FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
            if (result is null)
            {
                throw new NotFoundException();
            }
            return result;
        }


    }
}

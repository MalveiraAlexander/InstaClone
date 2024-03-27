using InstaClone.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IRepositories
{
    public interface IReactionTypeRepository
    {
        Task<ICollection<ReactionType>> GetAllAsync(CancellationToken cancellationToken);
        Task<ReactionType> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}

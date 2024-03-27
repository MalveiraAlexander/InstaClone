using InstaClone.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Commons.Interfaces.IServices
{
    public interface IReactionTypeService
    {
        Task<List<ReactionTypeResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<ReactionTypeResponse> GetByIdAsync(int id, CancellationToken cancellationToken);
    }
}

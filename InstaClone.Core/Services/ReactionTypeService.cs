using AutoMapper;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Interfaces.IServices;
using InstaClone.Commons.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Services
{
    public class ReactionTypeService : IReactionTypeService
    {
        private readonly IReactionTypeRepository repository;
        private readonly IMapper mapper;

        public ReactionTypeService(IReactionTypeRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<List<ReactionTypeResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<List<ReactionTypeResponse>>(await repository.GetAllAsync(cancellationToken));
        }

        public async Task<ReactionTypeResponse> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return mapper.Map<ReactionTypeResponse>(await repository.GetByIdAsync(id, cancellationToken));
        }
    }
}

using FluentValidation;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;
using InstaClone.Commons.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Validators
{
    public class PostReactionRequestValidator : AbstractValidator<PostReactionRequest>
    {
        public PostReactionRequestValidator(IGenericRepository<ReactionType> reactionTypeRepository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.ReactionTypeId)
                .NotEmpty()
                .Must(reactionTypeId =>
                {
                    return reactionTypeRepository.ExistByIdAsync(reactionTypeId).Result;
                }).WithMessage("Reaction type not found");

        }
    }
}

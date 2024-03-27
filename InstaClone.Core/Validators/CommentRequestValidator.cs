using FluentValidation;
using Microsoft.AspNetCore.Http;
using InstaClone.Commons.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Models;

namespace InstaClone.Core.Validators
{
    public class CommentRequestValidator : AbstractValidator<CommentRequest>
    {
        public CommentRequestValidator(IGenericRepository<Post> postRepository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.PostId)
                .NotEmpty()
                .Must(postId =>
                {
                    return postRepository.ExistByIdAsync(postId).Result;
                }).WithMessage("Post not found");
            RuleFor(p => p.Text).MinimumLength(1).NotEmpty().NotNull();
        }
    }
}

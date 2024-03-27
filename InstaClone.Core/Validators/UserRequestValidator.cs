using FluentValidation;
using InstaClone.Commons.Interfaces.IRepositories;
using InstaClone.Commons.Requests;
using InstaClone.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaClone.Core.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator(IUserRepository userRepository)
        {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(p => p.NickName)
                            .NotEmpty()
                            .Must(nickname =>
                            {
                                return userRepository.GetByNickNameAsync(nickname, default).Result is null;
                            }).WithMessage("Nickname already exist");
            RuleFor(p => p.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)[A-Za-z\\d]{6,}$")
                                    .WithMessage("Password needs a mix of upper, lower, numbers, and be 6+ chars.");
        }
    }
}

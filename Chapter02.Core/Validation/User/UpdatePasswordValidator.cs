using Chapter02.Core.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.User
{
    public class UpdatePasswordValidator : AbstractValidator<UpdatePasswordDto>
    {
        public UpdatePasswordValidator()
        {
            RuleFor(e => e.OldPassword)
            .NotEmpty().WithMessage("Old Password Can not be empty")
            .NotNull().WithMessage("Old Password Can not be empty");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cant be empty")
            .MinimumLength(8).WithMessage("Your password Min lenght is 6 symbol")
            .MaximumLength(16).WithMessage("Your password Max lenght is 20 symbol")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");

            RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("Confirm PasswordMust be Equal password");
        }
    }
}

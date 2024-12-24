using Chapter02.Core.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.User
{
    public class SignInUserValidator : AbstractValidator<SignInUserDto>
    {
        public SignInUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Wrong Email Format")
              .NotEmpty().WithMessage("Email Not to be empty").NotNull()
              .MaximumLength(128).WithMessage("Email Maximum 128 symbol");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password cant be empty")
            .MinimumLength(8).WithMessage("Your password Min lenght is 6 symbol")
            .MaximumLength(16).WithMessage("Your password Max lenght is 20 symbol")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
        }
    }
}

using Chapter02.Core.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.User
{
    public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Field cant be empty").EmailAddress().WithMessage("Wrong Format");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Field cant be empty").MinimumLength(6)
                .WithMessage("Min lenght is 6 symbol").MaximumLength(20).WithMessage("max lenght is 20 symbol");
            RuleFor(u => u.ConfirmPassword).Equal(u => u.Password);
        }
    }
}

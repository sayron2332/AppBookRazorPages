using Chapter02.Core.Dtos.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(25).MinimumLength(2).NotNull();
            RuleFor(x => x.Surname).NotEmpty().MaximumLength(30).MinimumLength(2).NotNull();
            RuleFor(x => x.Email).NotEmpty().WithMessage("Not can be Empty").NotNull()
            .MaximumLength(128).WithMessage("Maximum 128 symbol")
            .EmailAddress().WithMessage("Email addres not valid");

            RuleFor(x => x.PhoneNumber)
            .MinimumLength(9).WithMessage("PhoneNumber must not be less than 10 characters.")
            .MaximumLength(20).WithMessage("PhoneNumber must not exceed 50 characters.")
            .Matches(new Regex("^\\+?[1-9][0-9]{7,14}$")).WithMessage("PhoneNumber not valid");

          
        }
    }
}

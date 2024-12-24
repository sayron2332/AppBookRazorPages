using Chapter02.Core.Dtos.Authors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.Authors
{
    public class AuthorValidator : AbstractValidator<AuthorDto>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name).NotEmpty().NotNull().WithMessage("Name Not can be empty")
              .MinimumLength(3).WithMessage("Minimum name lenghth 3 symbol")
              .MaximumLength(25).WithMessage("Maximum name lenghth 25 symbol");
            RuleFor(a => a.Surname).NotEmpty().NotNull().WithMessage("Surname Not can be empty")
              .MinimumLength(3).WithMessage("Minimum surname lenghth 3 symbol")
              .MaximumLength(30).WithMessage("Maximum surname lenghth 25 symbol");
        }
    }
}

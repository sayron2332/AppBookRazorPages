using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter02.Core.Entities;

namespace Chapter02.Core.Validation.Categories
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.Name).NotEmpty().NotNull().WithMessage("Can not be empty")
            .MinimumLength(3).WithMessage("can not be less 3 symbol")
            .MaximumLength(17).WithMessage("can not be more 17 symbol");
        }
    }
}

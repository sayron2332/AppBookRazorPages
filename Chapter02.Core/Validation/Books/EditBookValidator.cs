using Chapter02.Core.Dtos.Book;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Validation.Books
{
    public class EditBookValidator : AbstractValidator<BookDto>
    {
        public EditBookValidator()
        {

            RuleFor(b => b.Age).NotEmpty().NotNull().WithMessage("Age not will be Empty")
           .GreaterThan(0).WithMessage("Age must Greater Than 0")
           .LessThan(DateTime.Now.Year + 1).WithMessage("Age is Greater than current year");

            RuleFor(b => b.Price).NotNull().NotEmpty().WithMessage("Price not will be Empty")
             .GreaterThan(0).WithMessage("Price must Greater Than 0");

            RuleFor(b => b.Name).NotEmpty().NotNull().WithMessage("Name not will be Empty")
            .MaximumLength(50).WithMessage("Book name maximum length 50 symbol")
            .MinimumLength(3).WithMessage("Book name minumum length 3 symbol");

            RuleFor(b => b.Description).NotEmpty().NotNull().WithMessage("Description not will be Empty")
            .MaximumLength(1000).WithMessage("Description name maximum length 1000 symbol")
            .MinimumLength(3).WithMessage("Description name minumum length 3 symbol");

            RuleFor(b => b.Leanguage).NotEmpty().NotNull().WithMessage("Leanguage not will be Empty")
           .MaximumLength(30).WithMessage("Leanguage name maximum length 30 symbol")
           .MinimumLength(3).WithMessage("Leanguage name minumum length 3 symbol");

          

        }
    }
}

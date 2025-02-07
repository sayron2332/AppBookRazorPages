using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.Books;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Chapter02.Pages.Admin.Books
{
    public class UpdateModel : PageModel
    {
        private readonly IBookService _bookService;
        public required MultiSelectList categoryList;
        public required MultiSelectList authorList;
        [BindProperty]
        public required BookDto Book { get; set; }
        public UpdateModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> OnGet(int Id)
        {
            
            Book = await _bookService.GetBookByIdWithIncludes(Id);
            await LoadCategoriesAndAuthors();
            if (Book is null)
            {
                return RedirectToPage("AllBooks");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile? photo, int[] authorsId, int[] categoriesId)
        {
            
            EditBookValidator validator = new EditBookValidator();
            ValidationResult validationResult = validator.Validate(Book);
            if (authorsId.Length == 0)
            {
                ModelState.AddModelError("AuthorsLink", "Please select Author");
            }
            if (categoriesId.Length == 0)
            {
                ModelState.AddModelError("CategoriesLink", "Please select category");
            }
            if (validationResult.IsValid && ModelState.IsValid)
            {
                var result = await _bookService.Update(photo!, Book, authorsId, categoriesId);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("AllBooks");
                }
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("AllBooks");
            }
            Book = await _bookService.GetBookByIdWithIncludes(Book.Id);
            await LoadCategoriesAndAuthors();
            return Page();
        }
        private async Task LoadCategoriesAndAuthors()
        {
            
            categoryList = new MultiSelectList(
                await _bookService.LoadCategories(),
                nameof(Category.Id),
                nameof(Category.Name),
                Book.CategoriesLink.Select(c => c.CategoryId)
            );

            authorList = new MultiSelectList(
                await _bookService.LoadAuthors(),
                nameof(AuthorDto.Id),
                nameof(AuthorDto.FullName),
                Book.AuthorsLink.Select(c => c.AuthorId)
            );

        }
    }
}

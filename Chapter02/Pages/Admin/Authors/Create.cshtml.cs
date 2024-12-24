using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.Authors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Authors
{
    public class CreateModel : PageModel
    {
        [BindProperty]
        public AuthorDto Author { get; set; } = null!;
        private readonly IAuthorService _authorService;
        public CreateModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(IFormFile photo) 
        {
            AuthorValidator validator = new AuthorValidator();
            ValidationResult validationResult = validator.Validate(Author);
            if (validationResult.IsValid)
            {

                var result = await _authorService.Create(photo,Author);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                else
                {
                    TempData["ErrorMessage"] = result.Message;
                }
                return RedirectToPage("AllAuthors");
            }
            return Page();
        }
    }
}

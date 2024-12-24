using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Validation.Authors;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Authors
{
    public class UpdateModel : PageModel
    {
        [BindProperty]
        public AuthorDto Author { get; set; } = null!;
        private readonly IAuthorService _authorService;
        public UpdateModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            var result = await _authorService.GetbyId(id);
            if (result.Success)
            {
                Author = (AuthorDto)result.Payload;
                return Page();
            }
            TempData["ErrorMessage"] = result.Message;
            return RedirectToPage("AllAuthors");

        }

        public async Task<IActionResult> OnPost(IFormFile photo)
        {
            AuthorValidator validator = new AuthorValidator();
            ValidationResult validationResult = validator.Validate(Author);
            if (validationResult.IsValid)
            {
                var result = await _authorService.Update(photo, Author);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("AllAuthors");
                }
               
                TempData["ErrorMessage"] = result.Message;
                return Page();
                
            }
            return Page();
        }
    }
}

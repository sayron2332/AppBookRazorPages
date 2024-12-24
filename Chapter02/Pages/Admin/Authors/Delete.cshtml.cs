using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Authors
{
    public class DeleteModel : PageModel
    {
        [BindProperty]
        public AuthorDto Author { get; set; } = null!;
        private readonly IAuthorService _authorService;
        public DeleteModel(IAuthorService authorService)
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
        public async Task<IActionResult> OnPost(int id)
        {
            var result = await _authorService.Delete(Author.Id);
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
    }
}

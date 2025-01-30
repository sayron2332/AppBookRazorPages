using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Authors
{
    [Authorize(Roles = "admin")]
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
            Author = await _authorService.GetbyId(id);
            if (Author is null)
            {
                TempData["ErrorMessage"] = "Author not found";
                return RedirectToPage("AllAuthors");
              
            }
            ViewData["SuccessMessage"] = "Take your Author"; 
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            var result = await _authorService.Delete(id);
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

using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Chapter02.Pages.Admin.Categories
{
    [Authorize(Roles ="admin")]
    public class DeleteModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        public required Category Category { get; set; }
        public DeleteModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> OnGet([FromRoute] int Id)
        {
            Category = await _categoryService.GetbyId(Id);
            if (Category is null)
            {
                TempData["ErrorMessage"] = "Not correct Id";
                return RedirectToPage("AllCategories");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int Id)
        {
            var result = await _categoryService.Delete(Id);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("AllCategories");
            }
            TempData["ErrorMessage"] = result.Message;
            return RedirectToPage("AllCategories");
        }
    }
}

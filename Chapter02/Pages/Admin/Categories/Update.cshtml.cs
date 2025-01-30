using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Validation.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Categories
{
    public class UpdateModel : PageModel
    {
        private readonly ICategoryService _categoryService;
        [BindProperty]
        public Category Category { get; set; } = null!;
        public UpdateModel(ICategoryService categoryService)
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
        public async Task<IActionResult> OnPost()
        {
            CategoryValidator validator = new();
            var validationResut = validator.Validate(Category);
            if (validationResut.IsValid)
            {
                var result = await _categoryService.Update(Category);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("AllCategories");
                }
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("AllCategories");
            }
            return Page();
        }
    }
}

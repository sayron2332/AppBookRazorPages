using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Validation.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Chapter02.Pages.Admin.Categories
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Category Category { get; set; } = null!;
        private readonly ICategoryService _categoryService;
        public CreateModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            CategoryValidator validator = new CategoryValidator();
            var validationResult = validator.Validate(Category);
            if (validationResult.IsValid)
            {
                await _categoryService.Create(Category);
                return RedirectToPage("AllCategories");
            }
            return Page();
         
        }
    }
}

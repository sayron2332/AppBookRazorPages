using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Categories
{
    [Authorize(Roles = "admin")]
    public class AllCategoriesModel : PageModel
    {
        
        public IEnumerable<Category> categories { get; set; } = null!;
        private readonly ICategoryService _categoryService;
        public AllCategoriesModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> OnGet()
        {
            categories = await _categoryService.GetAll();
            return Page();
        }
    }
}

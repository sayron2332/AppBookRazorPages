using Azure;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Categories
{
    [Authorize(Roles = "admin")]
    public class AllCategoriesModel : PageModel
    {
        public int CategoryCount { get; set; }
        public int PageNo { get; set; }
        public IEnumerable<Category> categories { get; set; } = null!;
        private readonly ICategoryService _categoryService;
        public AllCategoriesModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            return await LoadCategories("");
        }
        public async Task<IActionResult> OnPost(string searchString, int p = 1)
        {
            PageNo = p;
            return await LoadCategories(searchString);
        }

        private async Task<IActionResult> LoadCategories(string searchString)
        {
            if (String.IsNullOrWhiteSpace(searchString))
            {
                CategoryCount = await _categoryService.GetCount();
                categories = await _categoryService.GetListByPagination(PageNo);
            }
            else
            {
                PageNo = 1;
                categories = await _categoryService.GetListBySearchAndPagination(searchString, PageNo);
                CategoryCount = categories.Count();
                if (CategoryCount <= 0)
                {
                    CategoryCount = await _categoryService.GetCount();
                    categories = await _categoryService.GetListByPagination(PageNo);
                    TempData["ErrorMessage"] = "Category not found all categories loaded";
                }
            }
            return Page();
        }


    }
}

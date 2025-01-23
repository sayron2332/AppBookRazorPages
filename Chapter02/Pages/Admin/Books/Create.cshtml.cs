using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Validation.Books;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Chapter02.Pages.Admin.Books
{
    [Authorize(Roles = "admin")]
    public class CreateModel : PageModel
    {
        private readonly IBookService _bookservice;
        private readonly ICategoryService _categoryService;
        private readonly IAuthorService _authorService;
        [BindProperty]
        public CreateBookDto Book { get; set; } = null!;
        public CreateModel(IBookService bookService, IAuthorService authorService, ICategoryService categoryService)
        {
            _bookservice = bookService;
            _authorService = authorService;
            _categoryService = categoryService;
        }
       
        public async Task<IActionResult> OnGet([FromForm]string searchString)
        {
            
            await LoadCategoriesAndAuthors();
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile photo)
        {
            CreateBookValidator validator = new();
            var validationResult = validator.Validate(Book);
            if (validationResult.IsValid)
            {
               var result = await _bookservice.Create(photo,Book);
               if (result.Success)
               {
                   TempData["SuccessMessage"] = result.Message;
                   return RedirectToPage("AllBooks");
               }
               ViewData["ErrorMessage"] = result.Message;
            }
            await LoadCategoriesAndAuthors();
            return Page();
        }

       
        private async Task LoadCategoriesAndAuthors()
        {
            ViewData["categoryList"] = new SelectList(
                await _categoryService.GetAll(),
                nameof(Category.Id),
                nameof(Category.Name)
            );
            ViewData["authorList"] = new SelectList(
                await _authorService.GetAll(),
                nameof(AuthorDto.Id),
                nameof(AuthorDto.FullName)
            );

        }

    }
}

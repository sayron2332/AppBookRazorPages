using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Books
{
    public class AllBooksModel : PageModel
    {
        public  IEnumerable<Book> Books { get; set; } = null!;
        private readonly IBookService _bookService;
        public AllBooksModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> OnGet()
        {
            Books = await _bookService.GetAll();
            return Page();
        }
    }
}

using Azure;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Home
{
    public class ShopModel(IBookService bookService) : PageModel
    {
        private readonly IBookService _bookService = bookService;
        public int BookCount { get; set; }
        public int PageNo { get; set; }
        public required IEnumerable<BookDto> Books { get; set; }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            string searchString = TempData["searchString"]?.ToString() ?? "";
            return await LoadBooks(searchString);
        }

        public async Task<IActionResult> OnPost(string searchString, int p = 1)
        {
            PageNo = p;
            return await LoadBooks(searchString);
        }

        private async Task<IActionResult> LoadBooks(string searchString)
        {
            
            if (string.IsNullOrWhiteSpace(searchString))
            {
                Books = await _bookService.GetListByPagination(PageNo);
                BookCount = await _bookService.GetCount();
            }
            else
            {
                PageNo = 1;
                Books = await _bookService.GetListBySearchAndPagination(searchString, PageNo);
                BookCount = Books.Count();
                if (BookCount == 0)
                {
                    Books = await _bookService.GetListByPagination(PageNo);
                    BookCount = await _bookService.GetCount();
                    ViewData["ErrorMessage"] = "Book not found all books loaded";
                }
            }
            return Page();
        }
    }
}
    


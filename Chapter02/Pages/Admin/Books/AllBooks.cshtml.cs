using Azure;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Books
{
    [Authorize(Roles = "admin")]
    public class AllBooksModel : PageModel
    {
        public int BookCount { get; set; }
        public int PageNo { get; set; }
        public  IEnumerable<BookDto> Books { get; set; } = null!;
        private readonly IBookService _bookService;
        public AllBooksModel(IBookService bookService)
        {
            _bookService = bookService;
        }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            BookCount = await _bookService.GetCount();
            Books = await _bookService.GetListByPagination(PageNo);
            return Page();
        }
        public async Task<IActionResult> OnPost(string searchString, int p = 1)
        {
            PageNo = p;
            Books = await _bookService.GetListBySearchAndPagination(searchString, PageNo);
            BookCount = Books.Count();
            if (BookCount == 0)
            {
                TempData["ErrorMessage"] = "I cant find this author";
                return RedirectToPage("AllBooks");
            }
            return Page();
        }
    }
}

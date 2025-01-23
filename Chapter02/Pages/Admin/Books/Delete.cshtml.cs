using AutoMapper;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Books
{
    [Authorize(Roles = "admin")]
    public class DeleteModel(IBookService bookService) : PageModel
    {
        public required BookDto Book { get; set; }
        private readonly IBookService _bookService = bookService;

        public async Task<IActionResult> OnGet(int Id)
        {
            Book = await _bookService.GetbyId(Id);
            if (Book is null)
            {
                TempData["ErrorMessage"] = "This Book not found";
                return RedirectToPage("AllBooks");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int Id)
        {
            var result = await _bookService.Delete(Id);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("AllBooks");
            }
            TempData["ErrorMessage"] = result.Message;
            return RedirectToPage("AllBooks");
        }
    }
}

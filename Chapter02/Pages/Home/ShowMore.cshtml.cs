using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Home
{
    public class ShowMoreModel(ICartService cartService, IBookService bookService) : PageModel
    {
        private readonly ICartService _cartService = cartService;
        private readonly IBookService _bookService = bookService;

        public required BookDto Book { get; set; }
        public async Task<IActionResult> OnGet(int Id)
        {
            Book = await _bookService.GetbyId(Id);
            if (Book is null)
            {
                TempData["ErrorMessage"] = "This Book not found";
                return RedirectToPage("Shop");
            }
            return Page();
        }
        public async Task<IActionResult> OnPost(int cartId,int BookId)
        {
            var result = await _cartService.AddBookToCart(cartId, BookId);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage("Shop");
            }
            ViewData["ErrorMessage"] = result.Message;
            return Page();
        }
    }
}

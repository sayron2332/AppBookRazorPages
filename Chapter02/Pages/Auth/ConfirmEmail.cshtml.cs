using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Auth
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly IUserService _userService;
        public ConfirmEmailModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet(string userId,string token)
        {
           var result = await _userService.ConfirmEmailAsync(userId, token);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                return RedirectToPage(nameof(SignIn));
            }
            ViewData["ErrorMessage"] = result.Message;
            return Page();
        }
    }
}

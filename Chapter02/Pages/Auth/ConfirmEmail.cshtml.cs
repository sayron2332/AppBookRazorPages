using Chapter02.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Auth
{
    public class ConfirmEmailModel : PageModel
    {
        private readonly UserService _userService;
        public ConfirmEmailModel(UserService userService)
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

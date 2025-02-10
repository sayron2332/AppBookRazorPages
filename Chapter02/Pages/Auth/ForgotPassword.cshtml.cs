using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Chapter02.Pages.Auth
{
    public class ForgotPasswordModel(UserService userService) : PageModel
    {
        private readonly UserService _userService = userService;
        [EmailAddress]
        [BindProperty]
        public required string Email { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            var result = await _userService.SendResetPasswordEmailAsync(Email);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
            }
            else {
                TempData["ErrorMessage"] = result.Message;
            }
            return RedirectToPage("SignIn");
        }
    }
}

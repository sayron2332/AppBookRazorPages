using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Auth
{
    public class ResetPasswordModel(IUserService userService) : PageModel
    {
        private readonly IUserService _userService = userService;
        [BindProperty]
        public required ForgotPasswordDto ForgotPassword { get; set; }
        public IActionResult OnGet(string email, string token)
        {
          
            ViewData["Email"] = email;
            ViewData["Token"] = token;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
            ForgotPasswordValidator validator = new ForgotPasswordValidator();
            var validationResult = await validator.ValidateAsync(ForgotPassword);
            if (validationResult.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(ForgotPassword);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("SignIn");
                }
                TempData["ErrorMessage"] = result.Message;
                return RedirectToPage("SignIn");
            }
            ViewData["Email"] = ForgotPassword.Email;
            ViewData["Token"] = ForgotPassword.Token;
            return Page();
        }
    }
}

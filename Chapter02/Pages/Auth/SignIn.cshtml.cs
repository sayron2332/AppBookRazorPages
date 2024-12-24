using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;

namespace Chapter02.Pages.Auth
{

    [ValidateAntiForgeryToken]
    [EnableRateLimiting("StrongLimitation")]
    public class SignInModel : PageModel
    {
        [BindProperty]
        public SignInUserDto SignInUser { get; set; } = null!;
        private readonly UserService _userService;
        public SignInModel(UserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            ViewData["SuccessMessage"] = TempData["ConfirmEmailMessage"];
        }
        
        public async Task<IActionResult> OnPost(SignInUserDto SignInUser)
        {
            SignInUserValidator validator = new();
            var validationResult = validator.Validate(SignInUser);
            if (validationResult.IsValid)
            {
               var result = await _userService.SignInUserAsync(SignInUser);
               if (result.Success)
               {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("/home/index");
               }
               ViewData["ErrorMessage"] = result.Message;
               return Page();
            }
           
            return Page();
        }
    }
}

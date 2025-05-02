using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;


namespace Chapter02.Pages.User
{
    [Authorize]
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("StrongLimitation")]
    public class ProfileModel : PageModel
    {

        [BindProperty]
        public UpdatePasswordDto UpdateUserPassword { get; set; } = null!;
        private readonly IUserService _userService;
        public ProfileModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
             Page();
        }

        public async Task<IActionResult> OnPost()
        { 
            UpdatePasswordValidator validator = new();
            ValidationResult validationResult = validator.Validate(UpdateUserPassword);
            if (validationResult.IsValid)
            {
                var result = await _userService.UpdatePasswordAsync(UpdateUserPassword);
                if (result.Success)
                {
                  

                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("/Index");
                }

                ViewData["ErrorMessage"] = result.Message;
                if (result.Errors != null)
                {
                    ViewData["Error"] = result.Errors.ToArray()[0];
                }

            }
            return Page();
        }
    }
}

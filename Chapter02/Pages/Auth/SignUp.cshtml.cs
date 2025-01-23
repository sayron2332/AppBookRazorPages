using Ardalis.Specification;
using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;

namespace Chapter02.Pages.Auth
{
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("StrongLimitation")]
    public class SignUp : PageModel
    {
        private readonly UserService _userService;

        [BindProperty]
        public CreateUserDto RegisterUser { get; set; } = null!;
        public SignUp(UserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPost()
        {
          
            CreateUserValidator validator = new CreateUserValidator();
            var validationResult = await validator.ValidateAsync(RegisterUser);
           
            if (validationResult.IsValid)
            {
                if (!User.IsInRole("admin"))
                {
                    RegisterUser.Role = "user"; 
                }
                var serviceResult = await _userService.CreateAsync(RegisterUser);
                if (serviceResult.Success)
                {
                    TempData["SuccessMessage"] = serviceResult.Message;
                    if (User.IsInRole("admin"))
                    {
                        return RedirectToPage("/admin/users/AllUsers");
                    }

                    return RedirectToPage(nameof(SignIn));
                }
                ViewData["ErrorMessage"] = serviceResult.Message;
                
                return Page();
            }
           
            return Page();
        }
    }
}

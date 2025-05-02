using Ardalis.Specification;
using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
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
        private readonly IUserService _userService;

        [BindProperty]
        public CreateUserDto RegisterUser { get; set; } = null!;
        public SignUp(IUserService userService)
        {
            _userService = userService;
        }

        public void OnGet()
        {
        }
        
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            RegisterUser.Role = "user";
            CreateUserValidator validator = new CreateUserValidator();
            var validationResult = await validator.ValidateAsync(RegisterUser);
           
            if (validationResult.IsValid)
            {
                var serviceResult = await _userService.CreateAsync(file,RegisterUser);
                if (serviceResult.Success)
                {
                    TempData["SuccessMessage"] = serviceResult.Message;
                    return RedirectToPage(nameof(SignIn));
                }
                ViewData["ErrorMessage"] = serviceResult.Message;
                
                return Page();
            }
           
            return Page();
        }
    }
}

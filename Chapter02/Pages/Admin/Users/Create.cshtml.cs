using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Users
{
  
    public class CreateModel(IUserService userService) : PageModel
    {
        private readonly IUserService _userService = userService;
        [BindProperty]
        public required CreateUserDto CreateUser { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(IFormFile photo)
        {
            CreateUserValidator validator = new CreateUserValidator();
            var validationResult = await validator.ValidateAsync(CreateUser);

            if (validationResult.IsValid)
            {
                var serviceResult = await _userService.CreateAsync(photo, CreateUser);
                if (serviceResult.Success)
                {
                    TempData["SuccessMessage"] = serviceResult.Message;
                    return RedirectToPage("AllUsers");
                }
                ViewData["ErrorMessage"] = serviceResult.Message;

                return Page();
            }

            return Page();
        }
    }
}

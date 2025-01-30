using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Users
{
  
    public class CreateModel(UserService userService) : PageModel
    {
        private readonly UserService _userService = userService;
        [BindProperty]
        public required CreateUserDto CreateUser { get; set; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost()
        {
            CreateUserValidator validator = new CreateUserValidator();
            var validationResult = await validator.ValidateAsync(CreateUser);

            if (validationResult.IsValid)
            {
                var serviceResult = await _userService.CreateAsync(CreateUser);
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

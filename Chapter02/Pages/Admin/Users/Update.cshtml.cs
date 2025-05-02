using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Chapter02.Core.Validation.User;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Users
{
    public class UpdateModel : PageModel
    {
       
        [BindProperty(SupportsGet = true)]
        public UpdateUserDto UpdateUser { get; set; } = null!;

        private readonly IUserService _userService;
        public UpdateModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> OnGet(string Id)
        {
            var user = await _userService.GetUserByIdAsync(Id);
            if (user == null)
            {
                return RedirectToPage("Profile");
            }
            UpdateUser.Name = user.Name;
            UpdateUser.Surname = user.Surname;
            UpdateUser.PhoneNumber = user.PhoneNumber;
            UpdateUser.Email = user.Email!;
            return Page();
        }


        public async Task<IActionResult> OnPost(IFormFile photo)
        {
            UpdateUserValidator validator = new UpdateUserValidator();
            ValidationResult validationResult = validator.Validate(UpdateUser);
            if (validationResult.IsValid)
            {
                var result = await _userService.UpdateUserAsync(photo, UpdateUser);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = result.Message;
                    return RedirectToPage("AllUsers");
                }
                ViewData["ErrorMessage"] = result.Message;
            

            }
            return Page();
        }
    }
}

using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.RateLimiting;

namespace Chapter02.Pages.User
{
    [ValidateAntiForgeryToken]
    [EnableRateLimiting("StrongLimitation")]
    public class DeleteModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public UpdateUserDto UpdateUser { get; set; } = null!;

        private readonly IUserService _userService;
        public DeleteModel(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {
            var user = await _userService.GetUserByIdAsync(UpdateUser.Id!);
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
        public async Task<IActionResult> OnPost()
        {
            var result =  await _userService.DeleteByIdAsync(UpdateUser.Id!);
            if (result.Success)
            {
                TempData["SuccessMessage"] = result.Message;
                if(User.IsInRole("admin"))
                {
                    return RedirectToPage("/admin/users/AllUsers");
                }
                return RedirectToPage("profile");
            }
           
            TempData["ErrorMessage"] = result.Message;
            if (result.Errors != null)
            {
                TempData["Error"] = result.Errors.ToArray()[0];
            }
            return RedirectToPage("profile");
        }
    }
}

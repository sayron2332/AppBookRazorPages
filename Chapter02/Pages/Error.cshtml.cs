using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages
{
    public class ErrorModel : PageModel
    {
        private readonly UserService _userService;
        public ErrorModel(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {

           var reslut = await _userService.GetAllUsersAsync();
           return Page();

        }
        public void OnPost()
        {
        }
    }
}

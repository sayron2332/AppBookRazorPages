using Azure;
using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Home
{
    public class IndexModel : PageModel
    {
        public List<AdminUserDto> contactUsers { get; set; } = null!;
        private readonly UserService _userService;
        public IndexModel(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {

            contactUsers = await _userService.GetAllUsersAsync();
            return Page();
        }
      
    }
}

using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Admin.Users
{
    [Authorize(Roles = "admin")]
    public class AllUsers : PageModel
    {
        public IEnumerable<AdminUserDto> Users { get; set; } = null!;

        private readonly UserService _userService;
        public AllUsers(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet()
        {
            Users = await _userService.GetAllUsersAsync();
            return Page();
        }
    }
}

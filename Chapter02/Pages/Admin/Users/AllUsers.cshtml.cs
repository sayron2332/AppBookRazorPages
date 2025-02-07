using Azure;
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
        public int PageNo { get; set; }
        public int UserCount { get; set; }
        public required IEnumerable<UserDto> Users { get; set; }

        private readonly UserService _userService;
        public AllUsers(UserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            UserCount = await _userService.GetCountAsync();
           
            Users = await _userService.GetListByPagination(PageNo);
            return Page();
        }
        public async Task<IActionResult> OnPost(string searchString,int p = 1)
        {
            PageNo = p;
            Users = await _userService.GetListBySearchByAndPagination(searchString,PageNo);
            UserCount = Users.Count();
            if (UserCount == 0)
            {
                TempData["ErrorMessage"] = "I cant find this user";
                return RedirectToPage("AllUsers");
            }
            return Page();
        }
    }
}

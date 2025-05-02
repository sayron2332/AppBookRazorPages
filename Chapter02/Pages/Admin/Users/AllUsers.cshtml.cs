using Azure;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Interfaces;
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

        private readonly IUserService _userService;
        public AllUsers(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            return await LoadUsers("");
        }
        public async Task<IActionResult> OnPost(string searchString, int p = 1)
        {
            PageNo = p;
            return await LoadUsers(searchString);
        }

        private async Task<IActionResult> LoadUsers(string searchString)
        {
            if (String.IsNullOrWhiteSpace(searchString))
            {
                UserCount = await _userService.GetCountAsync();
                Users = await _userService.GetListByPagination(PageNo);
            }
            else
            {
                PageNo = 1;
                Users = await _userService.GetListBySearchByAndPagination(searchString, PageNo);
                UserCount = Users.Count();
                if (UserCount <= 0)
                {
                    UserCount = await _userService.GetCountAsync();
                    Users = await _userService.GetListByPagination(PageNo);
                    TempData["ErrorMessage"] = "User not found all Users loaded";
                }
               
            }
            return Page();
        }
    }
}

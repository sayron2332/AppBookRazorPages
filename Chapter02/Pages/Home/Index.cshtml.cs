using Azure;
using Chapter02.Core;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chapter02.Pages.Home
{
    public class IndexModel : PageModel
    {
       
        public void OnGet()
        {
        }
        public IActionResult OnPost(string searchString)
        {
            TempData["searchString"] = searchString;
            return RedirectToPage("Shop");
        }

    }
}

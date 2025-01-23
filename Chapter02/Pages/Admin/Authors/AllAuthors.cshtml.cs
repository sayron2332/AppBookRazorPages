using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace Chapter02.Pages.Admin.Authors
{
    [Authorize(Roles ="admin")]
    public class AllAuthorsModel : PageModel
    {
        [BindProperty]
        public IEnumerable<AuthorDto> Authors { get; set; } = null!;
        private readonly IAuthorService _authorService;

        public AllAuthorsModel(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        public async Task<IActionResult> OnGet()
        {
            Authors = await _authorService.GetAll();
            return Page();
        }
    }
}

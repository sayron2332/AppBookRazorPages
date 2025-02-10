using Azure;
using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections;
using System.Collections.Generic;


namespace Chapter02.Pages.Admin.Authors
{
    [Authorize(Roles = "admin")]
    public class AllAuthorsModel : PageModel
    {
        [BindProperty]
        public IEnumerable<AuthorDto> Authors { get; set; } = null!;
        private readonly IAuthorService _authorService;
        public int AuthorsCount { get; set; }
        public int PageNo { get; set; }
        public AllAuthorsModel(IAuthorService authorService)
        {

            _authorService = authorService;
        }
        public async Task<IActionResult> OnGet(int p = 1)
        {
            PageNo = p;
            return await LoadAuthors("");
        }
        public async Task<IActionResult> OnPost(string searchString,int p = 1)
        {
            PageNo = p;
            return await LoadAuthors(searchString);
        }

        private async Task<IActionResult> LoadAuthors(string searchString)
        {
            if (String.IsNullOrWhiteSpace(searchString))
            {
                AuthorsCount = await _authorService.GetCount();
                Authors = await _authorService.GetListByPagination(PageNo);
            }
            else
            {
                PageNo = 1;
                Authors = await _authorService.GetListBySearchAndPagination(searchString, PageNo);
                AuthorsCount = Authors.Count();
                if (AuthorsCount <= 0)
                {
                    AuthorsCount = await _authorService.GetCount();
                    Authors = await _authorService.GetListByPagination(PageNo);
                    TempData["ErrorMessage"] = "Author not found all Authors loaded";
                }
            }
            return Page();
        }
    }
}

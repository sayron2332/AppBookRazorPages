﻿using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface IBookService
    {
        public Task<BookDto> GetbyId(int Id);
        public Task<ServiceResponse> Create(IFormFile photo, CreateBookDto category);
        public Task<ServiceResponse> Delete(int Id);
        public Task<IEnumerable<BookDto>> GetAll();
        public Task<ServiceResponse> Update(IFormFile photo,BookDto model);
        public Task<BookDto> GetBookByIdWithIncludes(int Id);
        public Task<IEnumerable<Category>> LoadCategories();
        public Task<IEnumerable<AuthorDto>> LoadAuthors();
    }
}

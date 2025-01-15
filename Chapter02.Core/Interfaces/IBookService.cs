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
        public Task<ServiceResponse> GetbyId(int Id);
        public Task<ServiceResponse> Create(IFormFile photo, CreateBookDto category);
        public Task<ServiceResponse> Delete(int Id);
        public Task<IEnumerable<Book>> GetAll();
        public Task<ServiceResponse> Update(Book model);
        public Task<BookDto> GetBookByIdWithIncludes(int Id);
    }
}

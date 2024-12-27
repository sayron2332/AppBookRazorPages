using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface IAuthorService
    {
        public Task<ServiceResponse> GetbyId(int Id);
        public Task<ServiceResponse> Create(IFormFile photo, AuthorDto model);
        public Task<ServiceResponse> Delete(int Id);
        public Task<IEnumerable<AuthorDto>> GetAll();
        public Task<IEnumerable<Author>> GetListById(int[] Id);
        public Task<ServiceResponse> Update(IFormFile photo, AuthorDto model);
    }
}

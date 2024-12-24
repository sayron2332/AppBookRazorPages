using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface IBookService
    {
        public Task<Book> GetbyId(int Id);
        public Task Create(Book category);
        public Task<ServiceResponse> Delete(int Id);
        public Task<IEnumerable<Book>> GetAll();
        public Task<ServiceResponse> Update(Book model);
    }
}

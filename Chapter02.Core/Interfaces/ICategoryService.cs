using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface ICategoryService
    {
        public Task<Category> GetbyId(int Id);
        public Task Create(Category category);
        public Task<ServiceResponse> Delete(int Id);
        public Task<IEnumerable<Category>> GetAll();
        public Task<ServiceResponse> Update(Category model);
        public Task<IEnumerable<Category>> GetListById(int[] Id);
    }
}

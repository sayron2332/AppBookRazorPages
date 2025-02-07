using Chapter02.Core.Dtos.Authors;
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
        public Task<ICollection<Category>> GetListById(int[] Id);
        public Task<ICollection<Category>> GetListByIdAsTracking(int[] Id);
        public Task<int> GetCount();
        public Task<IEnumerable<Category>> GetListBySearchAndPagination(string searchString, int pageIndex, int pageSize = 10);
        public Task<IEnumerable<Category>> GetListByPagination(int pageIndex, int pageSize = 10);
    }
}

using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repository;
        public CategoryService(IRepository<Category> repository)
        {
            _repository = repository;
        }
        public async Task Create(Category model)
        {
            await _repository.Insert(model);
            await _repository.Save();
            
        }
        public async Task<ServiceResponse> Delete(int Id)
        {
            Category category = await GetbyId(Id);
            if (category is null)
            {
                return new ServiceResponse
                { 
                    Success = false,
                    Message = "Category Not found"
                };
            }
            await _repository.Delete(Id);
            await _repository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfuly deleted"
            };
        }
        public async Task<IEnumerable<Category>> GetAll()
        {
            IEnumerable<Category> result = await _repository.GetAll();
            if (result is null)
            {
                return null!;
            }
            return result;
        }
        public async Task<Category> GetbyId(int id)
        {
            var result = await _repository.GetByID(id);
            if (result is null)
            {
                return null!;
            }
            return result;
        }
        public async Task<ServiceResponse> Update(Category model)
        {
            var category = await _repository.GetByID(model.Id);
            if (category is null)
            {
                return new ServiceResponse 
                {
                    Success = false,
                    Message = "Category not found"
                };
            }
            category.Name = model.Name;
     
            await _repository.Update(model);
            await _repository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "Category successfuly updated"
            };
        }
        public async Task<ICollection<Category>> GetListByIdAsTracking(int[] Id)
        {
            var categories = await _repository.GetListBySpec(new CategorySpecification.GetListByIdAsTracking(Id));
            return (ICollection<Category>)categories;
        }

        public async Task<ICollection<Category>> GetListById(int[] Id)
        {
            var categories = await _repository.GetListBySpec(new CategorySpecification.GetListById(Id));
            return (ICollection<Category>)categories;
        }
        public async Task<int> GetCount()
            => await _repository.GetCount();
        public async Task<IEnumerable<Category>> GetListByPagination(
          int pageIndex, int pageSize = 10)
        {
            int skip = (pageIndex - 1) * pageSize;
            return await _repository.GetListBySpec(new CategorySpecification.GetListByPagination(skip, pageSize));
        }
        public async Task<IEnumerable<Category>> GetListBySearchAndPagination(
            string searchString, int pageIndex, int pageSize = 10)
        {
            int skip = (pageIndex - 1) * pageSize;
            return await _repository.GetListBySpec(new CategorySpecification.SearchAndPagination(searchString,skip, pageSize));
        }
    }
}

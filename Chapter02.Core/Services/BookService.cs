using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _repository;
        public BookService(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task Create(Book model)
        {
            await _repository.Insert(model);
            await _repository.Save();

        }

        public async Task<ServiceResponse> Delete(int Id)
        {
            Book Book = await GetbyId(Id);
            if (Book is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Book Not found"
                };
            }
            await _repository.Delete(Id);
            await _repository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "Book successfuly deleted"
            };
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            IEnumerable<Book> result = await _repository.GetAll();
            if (result is null)
            {
                return null!;
            }
            return result;
        }


        public async Task<Book> GetbyId(int id)
        {
            var result = await _repository.GetByID(id);
            if (result is null)
            {
                return null!;
            }
            return result;
        }


        public async Task<ServiceResponse> Update(Book model)
        {
            var Book = await _repository.GetByID(model.Id);
            if (Book is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Book not found"
                };
            }
           
            await _repository.Update(model);
            await _repository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "Book successfuly updated"
            };
        }
    }
}

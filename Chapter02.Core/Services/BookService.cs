using AutoMapper;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Dtos.Configuration;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        public BookService(IRepository<Book> repository, IConfiguration configuration, 
            IMapper mapper, IAuthorService authorService, ICategoryService categoryService)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _authorService = authorService;
            _categoryService = categoryService;
        }

        public async Task<ServiceResponse> Create(IFormFile photo, CreateBookDto model)
        {
            if (photo != null)
            {
                ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.AuthorImage);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                model.ImageName = fileName;
            }
            Book book = _mapper.Map<Book>(model);
          
            book.Authors = await _authorService.GetListById(model.AuthorsId);
            book.Categories = await _categoryService.GetListById(model.CategoriesId);

            await _repository.Insert(book);
            await _repository.Save();
            return new ServiceResponse
            { 
                Success = true,
                Message = "All are good"
            };

        }

        public async Task<ServiceResponse> Delete(int Id)
        {
            Book? Book = await _repository.GetByID(Id);
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


        public async Task<ServiceResponse> GetbyId(int id)
        {
            var result = await _repository.GetByID(id);
            if (result is null)
            {
                return null!;
            }
            return new ServiceResponse 
            { 
                Success = true,
                Message = "All are good",
                Payload = result
            };
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

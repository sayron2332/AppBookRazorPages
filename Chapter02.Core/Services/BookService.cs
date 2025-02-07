using AutoMapper;
using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Dtos.Configuration;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Services
{
    public class BookService : IBookService
    {
        private readonly IRepository<Book> _bookRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthorService _authorService;
        private readonly ICategoryService _categoryService;
        public BookService(IRepository<Book> repository, IConfiguration configuration, 
            IMapper mapper, IAuthorService authorService, ICategoryService categoryService)
        {
            _bookRepository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _authorService = authorService;
            _categoryService = categoryService;
        }
        public async Task<IEnumerable<Category>> LoadCategories()
            => await _categoryService.GetAll();
        public async Task<IEnumerable<AuthorDto>> LoadAuthors()
           => await _authorService.GetAll();
        public async Task<ServiceResponse> Create(IFormFile photo, CreateBookDto model)
        {
            if (photo != null)
            {
                ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.BookImage);
                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    await photo.CopyToAsync(fileStream);
                }
                model.ImageName = fileName;
            }

            Book book = _mapper.Map<Book>(model);
            await AddAuthorsAndCategoriesToBook(book, model.AuthorsId, model.CategoriesId);
            await _bookRepository.Insert(book);
            await _bookRepository.Save();
            return new ServiceResponse
            { 
                Success = true,
                Message = "All are good"
            };

        }
        private async Task AddAuthorsAndCategoriesToBook(Book book, int[] authorsId, int[] categoriesId)
        {
          
            var authors = await _authorService.GetListByIdAsTracking(authorsId);
            var categories = await _categoryService.GetListByIdAsTracking(categoriesId);

            book.AuthorsLink.Clear();
            book.CategoriesLink.Clear();

            foreach (var author in authors)
            {

                BookAuthor bookAuthors = new BookAuthor();
                bookAuthors.Book = book;
                bookAuthors.Author = author;
                book.AuthorsLink.Add(bookAuthors);
            }
            foreach (var category in categories)
            {
                BookCategory bookCategory = new BookCategory();
                bookCategory.Book = book;
                bookCategory.Category = category;
                book.CategoriesLink.Add(bookCategory);

            }
        }
        public async Task<ServiceResponse> Delete(int Id)
        {
            Book? Book = await _bookRepository.GetByID(Id);
            if (Book is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "currentBook Not found"
                };
            }
            if (Book.ImageName != "default.jpg")
            {
                ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;
                string image = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.BookImage, Book.ImageName);
                File.Delete(image);
            }
            await _bookRepository.Delete(Id);
            await _bookRepository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "currentBook successfuly deleted"
            };
        }
        public async Task<IEnumerable<BookDto>> GetAll()
        {
            IEnumerable<Book> result = await _bookRepository.GetAll();
            return result.Select(b => _mapper.Map<BookDto>(b));
        }
        public async Task<BookDto> GetBookByIdWithIncludes(int Id)
        {
            return _mapper.Map<BookDto>(await _bookRepository
                .GetItemBySpec(new BookSpecification.GetBookByIdWithIncludes(Id))); 
        }
        public async Task<BookDto> GetbyId(int id)
        {
            var result = await _bookRepository.GetByID(id);
            if (result is null)
            {
                return null!;
            }
            return _mapper.Map<BookDto>(result);
        }
        public async Task<ServiceResponse> Update(IFormFile photo,BookDto model, int[] authorsId, int[] categoriesId)
        {
            Book currentBook = _mapper.Map<Book>(await GetBookByIdWithIncludes(model.Id));
          
            if (currentBook is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "currentBook not found"
                };
            }

            if (photo != null)
            {
                ImageSettings imageSettings = _configuration.GetSection("ImageSettings").Get<ImageSettings>()!;

                if (currentBook.ImageName != "default.jpg")
                {
                    string oldImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.BookImage, currentBook.ImageName);
                    File.Delete(oldImage);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
                string upload = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageSettings.BookImage);


                using (var fileStream = new FileStream(Path.Combine(upload, fileName), FileMode.Create))
                {
                    photo.CopyTo(fileStream);
                }

                model.ImageName = fileName;
            }
            else
            {
                model.ImageName = currentBook.ImageName;
            }

            _mapper.Map(model, currentBook);

            await AddAuthorsAndCategoriesToBook(currentBook, authorsId, categoriesId);
            await _bookRepository.Update(currentBook);
            await _bookRepository.Save();

            return new ServiceResponse
            {
                Success = true,
                Message = "currentBook successfuly updated"
            };
        }
        public async Task<int> GetCount()
            => await _bookRepository.GetCount();

        public async Task<IEnumerable<BookDto>> GetListBySearchAndPagination(
           string searchString, int pageIndex, int pageSize = 10)
        {
            int skip = (pageIndex - 1) * pageSize;
            IEnumerable<Book> result = await _bookRepository.GetListBySpec(new BookSpecification.SearchAndPagination(searchString,skip, pageSize));
            return result.Select(a => _mapper.Map<BookDto>(a));
        }
        public async Task<IEnumerable<BookDto>> GetListByPagination(
          int pageIndex, int pageSize = 10)
        {
            int skip = (pageIndex - 1) * pageSize;
            IEnumerable<Book> result = await _bookRepository.GetListBySpec(new BookSpecification.GetListByPagination(skip, pageSize));
            return result.Select(a => _mapper.Map<BookDto>(a));
        }
    }
}

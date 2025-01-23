using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using Chapter02.Core.Interfaces;
using Chapter02.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Infrastructure.Repository
{
    public class BookRepository(AppDbContext context) : IBookRepository
    {
        private AppDbContext _context = context;
        
       
        public async Task Delete(object id)
        {
            Book? bookToDelete = await _context.Books.FindAsync(id);
            if (bookToDelete != null)
            {
                await Delete(bookToDelete);
            }
        }

        public async Task Delete(Book bookToDelete)
        {
            await Task.Run(
                () =>
                {
                    if (_context.Entry(bookToDelete).State == EntityState.Detached)
                    {
                        _context.Books.Attach(bookToDelete);
                    }
                    _context.Books.Remove(bookToDelete);
                });
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Book?> GetByID(object id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Book> GetItemBySpec(ISpecification<Book> specification)
        {
            var book = await ApplySpecification(specification).FirstOrDefaultAsync();
            return book!;
        }

        private IQueryable<Book> ApplySpecification(ISpecification<Book> specification)
        {
            var evaluator = new SpecificationEvaluator();
            return evaluator.GetQuery(_context.Books, specification);
        }

        public async Task<IEnumerable<Book>> GetListBySpec(ISpecification<Book> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }

        public async Task Insert(Book entity)
        {
            await _context.Books.AddAsync(entity);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task Update(Book oldBook, BookDto bookToUpdate)
        {

            var authors = await _context.BookAuthor.Where(b => b.BookId == bookToUpdate.Id).ToListAsync();
            if (authors != null)
            {
                foreach (BookAuthor item in authors)
                {
                    _context.BookAuthor.Remove(item);
                }
            }
            var categories = await _context.BookCategory.Where(b => b.BookId == bookToUpdate.Id).ToListAsync();
            if (authors != null)
            {
                foreach (var item in categories)
                {
                    _context.BookCategory.Remove(item);
                }
            }

            await Task.Run(() =>
            {
                _context.Books.Attach(oldBook);
                _context.Entry(oldBook).State = EntityState.Modified;
            });
        }
    }
}

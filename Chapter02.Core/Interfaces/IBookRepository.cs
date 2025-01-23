using Ardalis.Specification;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface IBookRepository
    {
        Task Save();
        Task<Book> GetItemBySpec(ISpecification<Book> specification);
        Task<IEnumerable<Book>> GetListBySpec(ISpecification<Book> specification);
        Task<IEnumerable<Book>> GetAll();
        Task<Book?> GetByID(object id);
        Task Insert(Book entity);
        Task Delete(object id);
        Task Delete(Book bookToDelete);
        Task Update(Book oldBook, BookDto bookToUpdate);
    }
}

using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface ICartService
    {
        public Task<ServiceResponse> CreateCart(Cart cart);
        public Task<ServiceResponse> AddBookToCart(int cartId, int bookId);
        public Task<ServiceResponse> DeleteCartById(int Id);
        public Task<Cart> GetByUserId(string userId);
    }
}

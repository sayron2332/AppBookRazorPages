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
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> _cartRepository;
        private readonly IBookService _bookService;
        public CartService(IRepository<Cart> repository, IBookService bookService)
        {
            _cartRepository = repository;
            _bookService = bookService;
        }
        public async Task<ServiceResponse> CreateCart(Cart cart)
        {
            await _cartRepository.Insert(cart);
            await _cartRepository.Save();
            return new ServiceResponse
            {
                Success = true,
                Message = "All is good"
            };
        }
        public async Task<Cart> GetByUserId(string userId)
        {
            var result = await _cartRepository.GetItemBySpec(new CartSpecification.GetByUserId(userId));
            return result!;
        }

        public async Task<ServiceResponse> DeleteCartById(int Id)
        {
            var cart = await _cartRepository.GetByID(Id);
            if (cart == null)
            {
                return new ServiceResponse
                {
                    Message = "Cart didnt find",
                    Success = false
                };
            }
            await _cartRepository.Delete(cart.Id);
            await _cartRepository.Save();
            return new ServiceResponse
            {
                Message = "Cart succsessfuly deleted",
                Success = true
            };
        }

        public async Task<ServiceResponse> AddBookToCart(int cartId, int bookId)
        {
            var cart = await _cartRepository.GetByID(cartId);
            if (cart == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Cart not found"
                };
            }
            var book = await _bookService.GetByIdAsTracking(bookId);
            cart.Books.Add(book);
            cart.Quantity++;
            await _cartRepository.Save();
            return new ServiceResponse
            {
                Success = false,
                Message = "Cart not found"
            };
        }
    }
}

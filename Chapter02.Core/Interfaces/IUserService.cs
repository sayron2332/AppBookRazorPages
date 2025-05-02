using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse> SignInUserAsync(SignInUserDto signInUser);
        public Task LogoutAsync();
        public Task<AspNetUser> GetUserByIdAsync(string id);
        public Task<int> GetCountAsync();
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<IEnumerable<UserDto>> GetListByPagination(int page, int pageSize = 10);
        public Task<IEnumerable<UserDto>> GetListBySearchByAndPagination(string searchString, int page, int pageSize = 10);
        public Task<ServiceResponse> CreateAsync(IFormFile photo, CreateUserDto registerUser);
        public Task<ServiceResponse> DeleteByIdAsync(string id);
        public Task<ServiceResponse> UpdateUserAsync(IFormFile photo, UpdateUserDto updateUser);
        public Task<ServiceResponse> UpdatePasswordAsync(UpdatePasswordDto updatePassword);
        public Task<ServiceResponse> ResetPasswordAsync(ForgotPasswordDto model);
        public Task<ServiceResponse> SendResetPasswordEmailAsync(string Email);
        public Task<ServiceResponse> ConfirmEmailAsync(string userId, string token);
    }
}

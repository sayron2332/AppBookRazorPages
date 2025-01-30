using AutoMapper;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.Text;
namespace Chapter02.Core.Services
{
    public class UserService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly EmailService _emailService;
        private readonly IConfiguration _config;
        private readonly IMapper _autoMapper;
        public UserService(UserManager<AspNetUser> userManager,
            RoleManager<IdentityRole> roleManager, EmailService emailService,
            SignInManager<AspNetUser> signInManager, IConfiguration config,
            IMapper autoMapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _config = config;
            _autoMapper = autoMapper;
        }

        public async Task<ServiceResponse> SignInUserAsync(SignInUserDto signInUser)
        {

            var user = await _userManager.FindByEmailAsync(signInUser.Email);
            if (user == null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found or email or password incorrect",
                };
            }
            var result = await _signInManager.PasswordSignInAsync(user, signInUser.Password,
                signInUser.RememberMe, lockoutOnFailure: true);

            if (result.Succeeded)
            {

                return new ServiceResponse
                {
                    Success = true,
                    Message = "You SignIn",
                };
            }
            else if (result.IsNotAllowed)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Confirm your email please."
                };
            }
            else if (result.IsLockedOut)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User is locked out. Connect with site administrator."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "User or password incorrect."
            };
        }
        public async Task LogoutAsync() => await _signInManager.SignOutAsync();
        public async Task<AspNetUser> GetUserByIdAsync(string id)
        {
            AspNetUser? user = await _userManager.FindByIdAsync(id);
            return user!;
        }
        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserDto> mappedUsers = new();
            foreach (AspNetUser user in users)
            {
                UserDto adminUser = _autoMapper.Map<UserDto>(user);
                IList<string> roles = await _userManager.GetRolesAsync(user);
                adminUser.Role = roles[0];
                mappedUsers.Add(adminUser);
            }
            return mappedUsers;
        }
        private async Task SendConfirmationEmail(AspNetUser newUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            var encodedEmailToken = Encoding.UTF8.GetBytes(token);
            var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

            var url = $"{_config["HostSettings:URL"]}/auth/confirmemail/{newUser.Id}/{validEmailToken}";
            string body = $"<h1>Confirm your email</h1> <a href='{url}'>Confirm now!</a>";
            await _emailService.SendEmailAsync(newUser.Email!, "Confirmation email.", body);
        }
        public async Task<ServiceResponse> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found."
                };
            }

            byte[] decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Email successfully confirmed."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Email not confirmed.",
                Errors = result.Errors.Select(e => e.Description)
            };

        }
        public async Task<ServiceResponse> CreateAsync(CreateUserDto registerUser)
        {
            var user = await _userManager.FindByEmailAsync(registerUser.Email);
            if (user != null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "This user already exist"
                };
            }

            AspNetUser mappedUser = _autoMapper.Map<AspNetUser>(registerUser);

            var result = await _userManager.CreateAsync(mappedUser, registerUser.Password);
            if (result.Succeeded)
            {

                await _userManager.AddToRoleAsync(mappedUser, registerUser.Role!);
                await SendConfirmationEmail(mappedUser);
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User succsesfly Created Please Verify your Email"
                };
            }

            List<IdentityError> listErrors = result.Errors.ToList();

            string errors = "";
            foreach (var error in listErrors)
            {
                errors = errors + error.Description.ToString();
            }

            return new ServiceResponse
            {
                Message = errors,
                Success = false,
            };
        }
        public async Task<ServiceResponse> DeleteByIdAsync(string id)
        {
            AspNetUser? user = await _userManager.FindByIdAsync(id);
            if (user is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User Not found"
                };
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User successfully deleted."
                };
            }

            return new ServiceResponse
            {
                Success = false,
                Message = "Sonething wrong",
                Errors = result.Errors.Select(e => e.Description)

            };
        }
        public async Task<ServiceResponse> UpdateUserAsync(UpdateUserDto updateUser)
        {
            AspNetUser? user = await _userManager.FindByIdAsync(updateUser.Id!);
            if (user is null)
            {
                return new ServiceResponse
                {
                    Success = false,
                    Message = "User not found",
                };
            }

            var currentRole = await _userManager.GetRolesAsync(user);
            if (updateUser.Role != currentRole[0])
            {
                await _userManager.RemoveFromRoleAsync(user, currentRole[0]);
                await _userManager.AddToRoleAsync(user, updateUser.Role);
            }

            user.Name = updateUser.Name;
            user.Surname = updateUser.Surname;
            user.PhoneNumber = updateUser.PhoneNumber;

            if (user.Email != updateUser.Email)
            {
                user.EmailConfirmed = false;
                user.Email = updateUser.Email;
                user.UserName = updateUser.Email;
                await SendConfirmationEmail(user);
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "User succsesfly Updated if you change your Email please Verify",
                };
            }
            return new ServiceResponse
            {
                Success = false,
                Message = "this email already exist",
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        public async Task<ServiceResponse> UpdatePasswordAsync(UpdatePasswordDto updatePassword)
        {
            var user = await _userManager.FindByIdAsync(updatePassword.Id!);
            if (user is null)
            {
                return new ServiceResponse 
                {
                    Success = false,
                    Message = "User not found"
                };
            }
            var result = await _userManager.ChangePasswordAsync(user
                ,updatePassword.OldPassword, updatePassword.Password);
            if (result.Succeeded)
            {
                return new ServiceResponse
                {
                    Success = true,
                    Message = "Password sucses change"
                };
            }
           
           
            return new ServiceResponse
            {
                Success = false,
                Message = "some error maybe old password not correct",
                Errors = result.Errors.Select(e => e.Description) 
        };
        }
     

    }
}

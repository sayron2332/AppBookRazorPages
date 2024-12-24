
using Chapter02.Core.AutoMapper.Authors;
using Chapter02.Core.AutoMapper.User;
using Chapter02.Core.Interfaces;
using Chapter02.Core.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Chapter02.Core
{
    public static class ServiceExtensions
    {
        public static void AddCoreServices(this IServiceCollection services)
        {
            services.AddScoped<UserService>();
            services.AddScoped<EmailService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
          
        }

        public static void AddAutoMappers(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperUserProfile));
            services.AddAutoMapper(typeof(AutoMapperAuthor));
        }
        public static void AddValidation(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

namespace Chapter02
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureRateLimmiter(this IServiceCollection services)
        { 
             services.AddRateLimiter(options =>
             {
                 options.AddSlidingWindowLimiter("Default", config =>
                 {
                     config.PermitLimit = 100; // Максимум 100 запитів
                     config.Window = TimeSpan.FromMinutes(1); // За хвилину
                     config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                     config.QueueLimit = 5; // Дозволено максимум 5 запити в черзі
                 });

                 options.AddSlidingWindowLimiter("LitleLimitation", config =>
                 {
                     config.PermitLimit = 30;
                     config.Window = TimeSpan.FromMinutes(1);
                     config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                     config.QueueLimit = 5;
                 });

                 options.AddSlidingWindowLimiter("StrongLimitation", config =>
                 {
                     config.PermitLimit = 5; 
                     config.Window = TimeSpan.FromMinutes(1); 
                     config.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                     config.QueueLimit = 3; 
                 });
             });
            return services;
        }
        public static IServiceCollection ConfigureCoockie(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/login";  // Шлях для редиректу при невдалій автентифікації
                options.AccessDeniedPath = "/error";  // Шлях при відмові в доступі
                options.Cookie.Name = "authCoockie"; // Ім'я cookie
                options.Cookie.HttpOnly = true;  // Доступно тільки через HTTP
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;  // Для HTTPS
                options.Cookie.SameSite = SameSiteMode.Strict; // Для захисту від CSRF атак
            });
            return services;
        }

    
    }
}

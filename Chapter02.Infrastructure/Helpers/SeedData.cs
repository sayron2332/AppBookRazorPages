using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace Chapter02.Infrastructure.Helpers
{
    internal static class SeedData
    {
        public static void SeedRolesUsersCommentsCarts(ModelBuilder builder)
        {
            string ADMIN_ID = Guid.NewGuid().ToString();
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();
            string USER_ROLE_ID = Guid.NewGuid().ToString();
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = ADMIN_ROLE_ID,
                Name = "admin",
                NormalizedName = "ADMIN"
            });
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = USER_ROLE_ID,
                Name = "user",
                NormalizedName = "USER"
            });

            builder.Entity<Cart>(b =>
            {
                b.HasData(
                    new Cart { Id = 1,  UserId = ADMIN_ID }
                );
            });

            var hasher = new PasswordHasher<AspNetUser>();
            builder.Entity<AspNetUser>().HasData(new AspNetUser
            {
                Id = ADMIN_ID,
                Name = "Nazar",
                Surname = "Kurylovych",
                UserName = "xvtnxjgbyv@gmail.com",
                NormalizedUserName = "xvtnxjgbyv@gmail.com",
                Email = "xvtnxjgbyv@gmail.com",
                NormalizedEmail = "xvtnxjgbyv@gmail.com",
                PhoneNumber = "+380959348105",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "Sayron561"),
                SecurityStamp = string.Empty,
                CartId = 1
             
            }) ;

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

          

            builder.Entity<Comment>(b =>
            {
                b.HasData(
                    new Comment { Id = 1, Name = "So cool Book about asp.net Core", NumberOfStars = 5, UserId = ADMIN_ID },
                    new Comment { Id = 2, Name = "So cool Book about Entity Framwork Core", NumberOfStars = 4,UserId = ADMIN_ID }
                );
            });
        }
        public static void SeedBooksAuthorsCategories(ModelBuilder builder)
        {

            builder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Andrew", Surname = "Lock" },
                new Author { Id = 2, Name = "Jon", Surname = "Smith" }
            );

            builder.Entity<Book>(b =>
            {


                b.HasData(
                    new Book
                    {
                        Id = 1,
                        Name = "Asp.Net Core in action, Third Edition",
                        Description = "You will learn all asp net core",
                        Age = 2021,
                        Leanguage = "English",
                        NumberOfPages = 819,
                        Price = 24
                    },
                    new Book
                    {
                        Id = 2,
                        Name = "Entity Framework core in action, Third Edition",
                        Description = "You will learn all Entity Framework core",
                        Age = 2020,
                        Leanguage = "English",
                        NumberOfPages = 920,
                        Price = 30
                    }
                );

            });

            builder.Entity<Category>(b =>
            {
                b.HasData(
                    new Category { Id = 1, Name = "Fantasy" },
                    new Category { Id = 2, Name = "Study" }
                );

            });

            builder.Entity<BookAuthor>(b =>
            {
                b.HasData(
                    new BookAuthor {AuthorId = 1, BookId = 1},
                    new BookAuthor {AuthorId = 2, BookId = 2 }
                );
            });

            builder.Entity<BookCategory>(b =>
            {
                b.HasData(
                    new BookCategory { CategoryId = 1, BookId = 1 },
                    new BookCategory { CategoryId = 2, BookId = 2 }
                );
            });
        }

    }
}

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
        public static void SeedRolesUsersComments(ModelBuilder builder)
        {
            string ADMIN_ID = Guid.NewGuid().ToString();
            // any guid, but nothing is against to use the same one
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
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null!, "Sayron450@"),
                SecurityStamp = string.Empty,
             
            }) ;

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

            builder.Entity<Comment>(b =>
            {
                b.HasData(
                    new Comment { Id = 1, Name = "So cool book about asp.net Core", NumberOfStars = 5 },
                    new Comment { Id = 2, Name = "So cool book about Entity Framwork Core", NumberOfStars = 4 });



                b.HasMany(x => x.Users)
                    .WithMany(x => x.Comments)
                    .UsingEntity(
                       "UserComment",
                        b => b.HasOne(typeof(AspNetUser)).WithMany().HasForeignKey("AspNetUserId").HasPrincipalKey(nameof(AspNetUser.Id)),
                        c => c.HasOne(typeof(Comment)).WithMany().HasForeignKey("CommentId").HasPrincipalKey(nameof(Comment.Id)),
                        je =>
                        {
                            je.HasKey("AspNetUserId", "CommentId");
                            je.HasData(
                                new { AspNetUserId = ADMIN_ID, CommentId = 1 });
                             
                        });
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
                    });

                b.HasMany(x => x.Authors)
                     .WithMany(x => x.Books)
                     .UsingEntity(
                         "BookAuthor",
                         r => r.HasOne(typeof(Author)).WithMany().HasForeignKey("AuthorId").HasPrincipalKey(nameof(Author.Id)),
                         l => l.HasOne(typeof(Book)).WithMany().HasForeignKey("BookId").HasPrincipalKey(nameof(Book.Id)),
                         je =>
                         {
                             je.HasKey("BookId", "AuthorId");
                             je.HasData(
                                 new { BookId = 1, AuthorId = 1 },
                                 new { BookId = 2, AuthorId = 2 });

                         });


            });

            builder.Entity<Category>(b =>
            {
                b.HasData(
                    new Category { Id = 1, Name = "Fantasy" },
                    new Category { Id = 2, Name = "Study" });


                b.HasMany(x => x.Books)
                    .WithMany(x => x.Categories)
                    .UsingEntity(
                        "BookCategory",
                        r => r.HasOne(typeof(Book)).WithMany().HasForeignKey("BookId").HasPrincipalKey(nameof(Book.Id)),
                        l => l.HasOne(typeof(Category)).WithMany().HasForeignKey("CategoryId").HasPrincipalKey(nameof(Category.Id)),
                        je =>
                        {
                            je.HasKey("CategoryId", "BookId");
                            je.HasData(
                                new { CategoryId = 2, BookId = 1 },
                                new { CategoryId = 2, BookId = 2 });

                        });
            });
        }

    }
}

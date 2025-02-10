using Chapter02.Core.Entities;
using Chapter02.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Chapter02.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AspNetUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            SeedData.SeedRolesUsersComments(builder);
            SeedData.SeedBooksAuthorsCategories(builder);

            builder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

            builder.Entity<BookCategory>()
           .HasKey(ba => new { ba.BookId, ba.CategoryId });


            builder.Entity<Book>()
            .HasIndex(b => b.Name);

            builder.Entity<Category>()
             .HasIndex(b => b.Name);

            builder.Entity<Author>()
           .HasIndex(b => b.Surname);


            base.OnModelCreating(builder);
        }
      
        public DbSet<AspNetUser> AspNetUsers { get; set; }
        public DbSet<Category> Catogories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<BookAuthor> BookAuthor { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }

    }
}

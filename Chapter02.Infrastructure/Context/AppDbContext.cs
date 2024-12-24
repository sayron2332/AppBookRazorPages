using Chapter02.Core.Entities;
using Chapter02.Infrastructure.Helpers;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Infrastructure.Context
{
    public class AppDbContext : IdentityDbContext<AspNetUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
          
            
            SeedData.SeedRolesUsersComments(builder);
            SeedData.SeedBooksAuthorsCategories(builder);
        
            


            base.OnModelCreating(builder);
        }
       

        DbSet<AspNetUser> AspNetUsers { get; set; }
        DbSet<Category> Catogories { get; set; }
        DbSet<Comment> Comments { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Author> Authors { get; set; }

    }
}

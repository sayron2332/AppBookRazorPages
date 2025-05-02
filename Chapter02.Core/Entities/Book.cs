using Chapter02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class Book : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Leanguage { get; set; } = string.Empty;
        public int Price { get; set; }
        public uint NumberOfPages { get; set; }
        public int Age { get; set; }
        public string ImageName { get; set; } = "default.jpg";
        public ICollection<BookCategory> CategoriesLink { get; set; } = [];
        public ICollection<BookAuthor> AuthorsLink { get; set; } = [];
        public ICollection<BookCart> CartLink { get; set; } = [];

    }
}

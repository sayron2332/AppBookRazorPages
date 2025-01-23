using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Dtos.Book
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageName { get; set; } = string.Empty;
        public string Leanguage { get; set; } = string.Empty;
        public int Price { get; set; }
        public uint NumberOfPages { get; set; }
        public int Age { get; set; }
        public ICollection<BookAuthor> AuthorsLink { get; set; } = [];
        public ICollection<BookCategory> CategoriesLink { get; set; } = [];
    }
}

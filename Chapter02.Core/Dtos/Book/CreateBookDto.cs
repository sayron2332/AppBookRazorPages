using Chapter02.Core.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Dtos.Book
{
    public class CreateBookDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Leanguage { get; set; } = string.Empty;
        public int Price { get; set; }
        public uint NumberOfPages { get; set; }
        public int Age { get; set; }
        public string ImageName { get; set; } = "default.jpg";
        public int[] CategoriesId { get; set; } = [];
        public int[] AuthorsId { get; set; } = [];
    }
}

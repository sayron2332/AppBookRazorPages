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
        public ICollection<AuthorDto> Authors { get; set; } = [];
        public ICollection<Category> Categories { get; set; } = [];
    }
}

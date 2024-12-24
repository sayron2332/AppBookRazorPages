using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class BookAuthor
    {
        public Book Book { get; set; } = null!;
        public Author Author { get; set; } = null!;
        public int AuthorId { get; set; }
        public int BookId { get; set; }
    }
}

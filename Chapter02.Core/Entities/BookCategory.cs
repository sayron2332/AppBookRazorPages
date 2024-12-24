using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class BookCategory
    {
        public Book Book { get; set; } = null!;
        public Category Category { get; set; } = null!;
        public int CategoryId { get; set; }
        public int BookId { get; set; }
    }
}

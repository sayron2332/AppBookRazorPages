using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class BookCart
    {
        public Book Book { get; set; } = null!;
        public Cart Cart { get; set; } = null!;
        public int BookId { get; set; }
        public int CartId { get; set; }
    }
}

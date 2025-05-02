using Chapter02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class Cart : IEntity
    {
        public int Id { get; set; }
        public int Quantity { get; set; } = 0;
        public ICollection<Book> Books { get; set; } = null!;
        public AspNetUser User { get; set; } = null!;
        public string UserId { get; set; } = string.Empty;
    }
}

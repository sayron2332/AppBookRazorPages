using Chapter02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class Author : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string ImageName { get; set; } = "default.jpg";
        public IEnumerable<Book> Books { get; set; } = null!;
    }
}

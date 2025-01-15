using Chapter02.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte NumberOfStars { get; set; }
        public ICollection<AspNetUser> Users { get; set; } = null!;
    }
}

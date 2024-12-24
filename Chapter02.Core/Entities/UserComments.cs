using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Entities
{
    public class UserComments
    {
        public AspNetUser User { get; set; } = null!;
        public Comment Comment { get; set; } = null!;
        public int CommentId { get; set; }
        public int UserId { get; set; }
    }
}

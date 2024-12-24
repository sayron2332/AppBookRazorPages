using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Dtos.Users
{
    public class UpdatePasswordDto
    {
        public string Id { get; set; } = string.Empty;
        public string OldPassword { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}

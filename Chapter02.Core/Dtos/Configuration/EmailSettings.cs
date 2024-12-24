using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Dtos.Configuration
{
    public class EmailSettings
    {
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string SMTP { get; set; } = string.Empty;
        public string PORT { get; set; } = string.Empty;
    }
}

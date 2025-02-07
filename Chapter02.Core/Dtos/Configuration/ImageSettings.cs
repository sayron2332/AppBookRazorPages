using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Dtos.Configuration
{
    public class ImageSettings
    {
        public string AuthorImage { get; set; } = string.Empty;
        public string BookImage { get; set; } = string.Empty;
        public string UserImage { get; set; } = string.Empty;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter02.Core.Interfaces;
using Microsoft.AspNetCore.Identity;


namespace Chapter02.Core.Entities
{
    public class AspNetUser : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public IEnumerable<Comment> Comments { get; set; } = null!;
    }
}

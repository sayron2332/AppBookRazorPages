using Ardalis.Specification;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Chapter02.Core.Specification
{
    public class CartSpecification
    {
        public class GetByUserId : Specification<Cart>
        {
            public GetByUserId(string userId)
            {
                Query.Where(c => c.UserId == userId);
            }
        }
    }
}

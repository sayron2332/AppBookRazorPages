using Ardalis.Specification;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Specification
{
    public class CategorySpecification
    {
        public class GetListById : Specification<Category>
        {
            public GetListById(int[] id)
            {
                Query.Where(c => id.Contains(c.Id));
            }
        }
        public class GetListByIdAsTracking : Specification<Category>
        {
            public GetListByIdAsTracking(int[] id)
            {
                Query.Where(a => id.Contains(a.Id)).AsTracking();
            }
        }
    }
}

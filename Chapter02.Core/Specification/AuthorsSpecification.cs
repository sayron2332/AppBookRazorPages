using Ardalis.Specification;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Specification
{
    public class AuthorsSpecification 
    {
        public class GetListById : Specification<Author>
        {
            public GetListById(int[] id)
            {
               Query.Where(a => id.Contains(a.Id));
            }
        }
        public class GetListByIdAsTracking : Specification<Author>
        {
            public GetListByIdAsTracking(int[] id)
            {
                Query.Where(a => id.Contains(a.Id)).AsTracking();
            }
        }
    }
}

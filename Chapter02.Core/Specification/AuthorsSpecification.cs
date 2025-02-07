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
                Query.AsTracking().Where(a => id.Contains(a.Id)).AsTracking();
            }
        }
        public class GetListByPagination : Specification<Author>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.Skip(skip).Take(take).OrderBy(c => c.Id);
            }
        }
        public class SearchAndPagination : Specification<Author>
        {
            public SearchAndPagination(string searchString, int skip, int take)
            {
                Query.Where(p => p.Surname.Contains(searchString))
                .Skip(skip).Take(take).OrderBy(p => p.Id);
            }
        }
    }
}

using Ardalis.Specification;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.Specification
{
    public class BookSpecification
    {
      
        public class GetBookByIdWithIncludes : Specification<Book>
        {
            public GetBookByIdWithIncludes(int id)
            {
                Query.Include(a => a.AuthorsLink).Include(c => c.CategoriesLink).Where(b => b.Id == id);
            }
        }
        public class GetByIdAsTracking : Specification<Book>
        {
            public GetByIdAsTracking(int id)
            {
                Query.AsTracking().Where(b => b.Id == id);
            }
        }
        public class GetListByPagination : Specification<Book>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.OrderBy(c => c.Id).Skip(skip).Take(take);
            }
        }
        public class SearchAndPagination : Specification<Book>
        {
            public SearchAndPagination(string searchString,int skip, int take)
            {
            
                Query.Where(p => p.Name.StartsWith(searchString)).OrderBy(p => p.Id)
                     .Skip(skip)
                     .Take(take);
            }
        }


    }
}

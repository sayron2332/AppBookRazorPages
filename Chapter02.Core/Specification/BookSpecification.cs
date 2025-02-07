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
        public class GetListByPagination : Specification<Book>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.Skip(skip).Take(take).OrderBy(c => c.Id);
            }
        }
        public class SearchAndPagination : Specification<Book>
        {
            public SearchAndPagination(string searchString,int skip, int take)
            {
                 Query
                 .Where(p => p.Name.Contains(searchString))
                 .Skip(skip).Take(take).OrderBy(p => p.Id);
            }
        }


    }
}

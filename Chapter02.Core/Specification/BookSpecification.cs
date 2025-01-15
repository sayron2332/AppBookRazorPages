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
                Query.Include(a => a.Authors).Include(c => c.Categories).Where(b => b.Id == id);
            }
        }
    }
}

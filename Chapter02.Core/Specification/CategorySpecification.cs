﻿using Ardalis.Specification;
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
        public class GetListByPagination : Specification<Category>
        {
            public GetListByPagination(int skip, int take)
            {
                Query.OrderBy(c => c.Id).Skip(skip).Take(take);
            }
        }
        public class SearchAndPagination : Specification<Category>
        {
            public SearchAndPagination(string searchString, int skip, int take)
            {
                Query.Where(p => p.Name.StartsWith(searchString)).OrderBy(p => p.Id)
                      .Skip(skip)
                      .Take(take);
            }
        }


    }
}

using AutoMapper;
using Chapter02.Core.Dtos.Authors;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.AutoMapper.Authors
{
    public class AutoMapperAuthor : Profile
    {
        public AutoMapperAuthor()
        {
            CreateMap<AuthorDto, Author>().ReverseMap();
        }
    }
}

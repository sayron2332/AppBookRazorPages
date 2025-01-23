using AutoMapper;
using Chapter02.Core.Dtos.Book;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.AutoMapper.Books
{
    public class AutoMapperBook : Profile
    {
        public AutoMapperBook()
        {
            CreateMap<CreateBookDto, Book>();
            CreateMap<Book, BookDto>().ReverseMap();
            
        }
    }
}

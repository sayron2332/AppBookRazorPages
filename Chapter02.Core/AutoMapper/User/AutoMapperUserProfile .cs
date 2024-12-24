using AutoMapper;
using Chapter02.Core.Dtos.Users;
using Chapter02.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02.Core.AutoMapper.User
{
    internal class AutoMapperUserProfile : Profile
    {
        public AutoMapperUserProfile()
        {
            CreateMap<CreateUserDto, AspNetUser>()
                .ForMember(dst => dst.UserName, act => act.MapFrom(src => src.Email));
            CreateMap<AdminUserDto, AspNetUser>().ReverseMap();
               

        }
    }
}

using AutoMapper;
using Finaktiva.Repository.Entities;
using Finaktiva.Services.ModelView;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finaktiva.Services.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModelView>().ReverseMap();
            CreateMap<Role, RoleModelView>().ReverseMap();
        }
    }
}

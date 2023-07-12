using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Dtos;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, RegisterCommand>().ReverseMap();
            CreateMap<User, UserForRegisterDto>().ReverseMap();
            
   
        }
    }
}

using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using AutoMapper;
using Core.Security.Identity;
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
            CreateMap<AppUser, UserForRegisterDto>().ReverseMap();
            CreateMap<AppUser, RegisterCommand>().ReverseMap();
            CreateMap<AppUser, OneTimePasswordDto>().ReverseMap();
            CreateMap<AppUser, LoginCommand>().ReverseMap();


        }
    }
}

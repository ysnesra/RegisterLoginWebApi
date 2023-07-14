using Application.Features.Auths.Dtos;
//using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Infrastructure.Identity;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands
{
    public class RegisterCommand : IRequest<UserForRegisterDto>
    { 
        public string NameSurname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, UserForRegisterDto>
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IMapper _mapper;

            public RegisterCommandHandler(UserManager<AppUser> userManager, IMapper mapper)
            {
                _userManager = userManager;
                _mapper = mapper;
            }

            public async Task<UserForRegisterDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                UserForRegisterDto createdUserDto = new UserForRegisterDto();
                AppUser existEmail = await _userManager.FindByEmailAsync(request.Email);
                if(existEmail != null)
                {
                    throw new EmailCanNotBeDuplicated(); //Email Tekrar girilemez hatası
                }


               
                
                //HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                AppUser newUser = _mapper.Map<AppUser>(request);
                newUser.PasswordHash = PasswordToolKit.EnhancedHashPassword(request.Password);
               
                newUser.Id=Guid.NewGuid().ToString();

                IdentityResult createdUser = await _userManager.CreateAsync(newUser,request.Password);
                var error = createdUser.Errors;
             
                if (createdUser.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(request.Email);  
                     createdUserDto = _mapper.Map<UserForRegisterDto>(user);                 
                }
                else
                {
                    throw new RegisterFailedException();
                }
                return createdUserDto;
            }
        }
    }

}


using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Identity;
using Core.Security.Dtos;
using Core.Security.Hashing;
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
        public string PhoneNumber { get; set; }

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
               
                AppUser newUser = _mapper.Map<AppUser>(request);
                newUser.PasswordHash = PasswordHashingHelper.EnhancedHashPassword(request.Password);              
                newUser.Id=Guid.NewGuid().ToString();

                //IdentityyServerda kullanıcı oluşturuken CreateAsyc metotu kull            
                IdentityResult createdUser = await _userManager.CreateAsync(newUser,request.Password);   
             
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


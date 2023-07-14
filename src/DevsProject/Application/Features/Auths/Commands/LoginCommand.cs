using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
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
using System.Linq.Dynamic.Core.Tokenizer;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Commands
{
    /// <summary>
    /// Kullanıcı giriş işlemini gerçekleştiren komut sınıfı
    /// </summary>
    public class LoginCommand : IRequest<LoginedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        /// <summary>
        /// Kullanıcı giriş işlemini gerçekleştiren işleyici sınıfı
        /// </summary>
        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginedDto>
        {

            private readonly AuthBusinessRules _authBusinessRules;
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager;
            private readonly ITokenHandler _tokenHandler;
            private readonly IAuthService _authService;
            private readonly IMapper _mapper;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IAuthService authService, IMapper mapper)
            {
                _authBusinessRules = authBusinessRules;
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenHandler = tokenHandler;
                _authService = authService;
                _mapper = mapper;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserShouldBeExistWhenLogin(request.UserForLoginDto.Email);

                AppUser? user = await _userManager.FindByEmailAsync(request.UserForLoginDto.Email);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.UserForLoginDto.Password, false);

                if (result.Succeeded) //Authentication başarılı!
                {
                    //Yetkiler belirlenir:
                    AccessToken createdAccessToken = _tokenHandler.CreateAccessToken(5,user);
             
                    return new LoginedDto()
                    {
                        AccessToken = createdAccessToken,
               
                    };
                }
                throw new AuthenticationErrorException();
    
            }
        }
    }
}
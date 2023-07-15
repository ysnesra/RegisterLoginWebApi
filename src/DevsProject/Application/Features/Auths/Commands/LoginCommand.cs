using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.Identity;
using Core.Security.JWT;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public OneTimePasswordDto OneTimePasswordDto { get; set; }



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
            private readonly ITwoFactorAuthenticationRepository _twoFactorAuthenticationRepository;


            public LoginCommandHandler(AuthBusinessRules authBusinessRules, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler, IAuthService authService, ITwoFactorAuthenticationRepository twoFactorAuthenticationRepository)
            {
                _authBusinessRules = authBusinessRules;
                _userManager = userManager;
                _signInManager = signInManager;
                _tokenHandler = tokenHandler;
                _authService = authService;
                _twoFactorAuthenticationRepository = twoFactorAuthenticationRepository;
            }

            public async Task<LoginedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {



                var oneTimePasswordInfo = await _twoFactorAuthenticationRepository.Query().FirstOrDefaultAsync(_ => _.Id == request.OneTimePasswordDto.OneTimePasswordId);

                if(oneTimePasswordInfo is null)
                    throw new Exception("Geçersiz OTP");

                if (oneTimePasswordInfo.OneTimePassword != request.OneTimePasswordDto.OneTimePassword)
                        throw new Exception("Geçersiz OTP");

                if (oneTimePasswordInfo is null)
                    throw new Exception("Geçersiz OTP");

                AppUser? user = await _userManager.FindByIdAsync(oneTimePasswordInfo.UserId.ToString());



                var verifyOtp = await _twoFactorAuthenticationRepository.VerifyOtp(Guid.Parse(user.Id), request.OneTimePasswordDto.OneTimePassword);

                if (!verifyOtp)
                    throw new Exception("Geçersiz OTP");


                //Yetkiler belirlenir:
                AccessToken createdAccessToken = _tokenHandler.CreateAccessToken(5, user);

                return new LoginedDto()
                {
                    AccessToken = createdAccessToken,

                };

                throw new AuthenticationErrorException();

            }
        }
    }
}
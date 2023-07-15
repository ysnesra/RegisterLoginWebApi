using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Enums;
using Core.Security.Identity;
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
    public class OtpLoginCommand : IRequest<OneTimePasswordDto>
    {
        public OneTimePasswordChannel Channel { get; set; }
       
        public string Email { get; set; }

        public string Password { get; set; }
        public class OtpLoginCommandHandler : IRequestHandler<OtpLoginCommand, OneTimePasswordDto>
        {
            private readonly ITwoFactorAuthenticationRepository _twoFactorAuthenticationRepository;
            private readonly UserManager<AppUser> _userManager;
            private readonly SignInManager<AppUser> _signInManager; 
            private readonly AuthBusinessRules _authBusinessRules;



            public OtpLoginCommandHandler(UserManager<AppUser> userManager, ITwoFactorAuthenticationRepository twoFactorAuthenticationRepository, AuthBusinessRules authBusinessRules, SignInManager<AppUser> signInManager)
            {
                _userManager = userManager;
                _twoFactorAuthenticationRepository = twoFactorAuthenticationRepository;
                _authBusinessRules = authBusinessRules;
                _signInManager = signInManager;
            }




            public async Task<OneTimePasswordDto> Handle(OtpLoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.UserShouldBeExistWhenLogin(request.Email);

                AppUser? user = await _userManager.FindByEmailAsync(request.Email);

                SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded) 
                {
                    throw new Exception("Kullanıcı adı veya şifre hatalı.");
                }

                var otpResult = await _twoFactorAuthenticationRepository.CreateOtp(Guid.Parse(user.Id), user.Email, request.Channel);

                return otpResult;
            }
        }
    }
}

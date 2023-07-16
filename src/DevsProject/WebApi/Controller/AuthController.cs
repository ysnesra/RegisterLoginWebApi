using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using Core.Security.Identity;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Core.Security.Enums;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        
        /// <summary>
        /// Kullanıcı kayıt işlemini yapar.
        /// </summary>
        /// <param name="RegisterCommand">Kullanıcı kayıt bilgileri.</param>
        /// <returns>Kullanıcı kayıt işleminin sonucunu döndürür.</returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand registerCommand)
        {
            //CreateLanguageCommand clasında-> Mediator; Handle'nı bulur ve Dto tipinde sonuç döner
            UserForRegisterDto result = await Mediator.Send(registerCommand);
            return Created("", result);
        }

        [HttpPost("OtpLogin")]
        public async Task<IActionResult> OtpLogin([FromBody] OtpLoginCommand otpLoginCommand)
        {           
            OneTimePasswordDto result = await Mediator.Send(otpLoginCommand);            
            return Created("", result);
        }

        /// <summary>
        /// Kullanıcı giriş işlemini yapar.
        /// </summary>
        /// <param name="userForLoginDto">Kullanıcı giriş bilgileri.</param>
        /// <returns>Kullanıcı giriş işleminin sonucunu döndürür.</returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(OneTimePasswordDto oneTimePasswordDto)
        {
            var loginCommand = new LoginCommand
            {
                OneTimePasswordDto = oneTimePasswordDto,
            };

            LoginedDto result = await Mediator.Send(loginCommand);
            //SetRefreshTokenToCookie(result.RefreshToken);
            return Created("", result.AccessToken);
        }


    }
}

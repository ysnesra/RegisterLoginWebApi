using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using Core.Infrastructure.Identity;
using Core.Security.Dtos;
using Core.Security.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

    }
}

using Application.Features.Auths.Commands;
using Application.Features.Auths.Dtos;
using Core.Infrastructure.Identity;
using Core.Security.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected UserManager<AppUser> _userManager { get; }

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }




        [HttpPost("Register")]
        public async Task<IActionResult> Register()
        {
            AppUser appUser = new AppUser();
            appUser.UserName = "Esra";
            appUser.Email = "ysnesra@gmail.com";
            appUser.PhoneNumber = "05389360909";

            IdentityResult result = await _userManager.CreateAsync(appUser,"12345");
              return Ok();
        }




    }

  
}

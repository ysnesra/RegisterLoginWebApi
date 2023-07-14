using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Infrastructure.Identity;
using Core.Security.Entities;
using Core.Security.Hashing;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly UserManager<AppUser> _userManager;
        public AuthBusinessRules(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        //Aynı mail adresinde kayıt var mı kontrolü
        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null) throw new BusinessException("Mail already exists");
        }

        //Kullanıcı Login olurken maili sistemde kayıtlı mı
        public async Task UserShouldBeExistWhenLogin(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user is null)
                throw new BusinessException("There is no such user record.");
        }

        //Kullanıcının şifresi doğru mu
        public void CheckIfPasswordIsCorrect(string requestPassword, string userPasswordHash, byte[] userPasswordSalt)
        {
            
            if (!HashingHelper.VerifyPasswordHash(requestPassword, userPasswordHash, userPasswordSalt))
                throw new BusinessException("Password isn't correct");
        }
    }
}

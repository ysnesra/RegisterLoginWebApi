using Application.Services.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using Core.Security.Hashing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Persistence.Utilities.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class TwoFactorAuthenticationRepository : EfRepositoryBase<TwoFactorAuthenticationTransaction, AppIdentityDbContext>, ITwoFactorAuthenticationRepository
    {  
        public TwoFactorAuthenticationRepository(AppIdentityDbContext context) : base(context)
        {
        }

        //Opt(OneTimePAssword üretme)
        public async Task<OneTimePasswordDto> CreateOtp(Guid userId, string to, OneTimePasswordChannel channel)
        {
            var result = new OneTimePasswordDto();

            var oneTimePassword = RandomGenerator.RandomOneTimePassword();

            //OneTimePasswordDto tipinde gelen veriler TwoFactorAuthenticationTransaction tipine çevrilir:
            var twoFactorAuthentication = new TwoFactorAuthenticationTransaction()
            {
                UserId = userId,
                Channel = channel,
                To = to,
                OneTimePassword = oneTimePassword,
                IsUsed = false,
                IsSend = false,
            };

            Context.Add(twoFactorAuthentication);
            await Context.SaveChangesAsync();

            result.OneTimePasswordId = twoFactorAuthentication.Id;

            return result;
        }

        //Otp Onaylama //Login olurken kullanılcak
        public async Task<bool> VerifyOtp(Guid userId, string oneTimePassword)
        {
            var dbTwoFactorAuthenticationTransactions =  Query()
                .Where(_ => _.UserId == userId)
                .OrderByDescending(_=>_.Id)
                .AsQueryable();

            var otpTransaction =  await dbTwoFactorAuthenticationTransactions.FirstOrDefaultAsync();

            //kullanıcının sondoğrulama kodu varmı
            if (otpTransaction is null)  
            {
                return false;
            }

            //kullanıcı var mı
            if (otpTransaction.IsUsed)   
            { 
                return false;
            }

            //Kullanıcının girdiği Onaykodu ile twoFactorAuthentication tablosundaki OnayKodu aynı mı
            if (otpTransaction.OneTimePassword != oneTimePassword)  
            {
                return false;
            }

            
            otpTransaction.IsUsed = true;   //Otp kulalnıldığından true ya çekildi
          
            Context.Update(otpTransaction);
            await Context.SaveChangesAsync();

            return true;

        }
    }
}

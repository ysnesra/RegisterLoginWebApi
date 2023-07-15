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

            result.Success = true;

            return result;
        }

        //Otp Onaylama
        public async Task<bool> VerifyOtp(Guid userId, string oneTimePassword)
        {
            var oneTimePasswordTransaction =  Query()
                .Where(_ => _.UserId == userId)
                .OrderByDescending(_=>_.Id)
                .AsQueryable();

            var otpTransaction =  await oneTimePasswordTransaction.FirstOrDefaultAsync();

            if(otpTransaction is null)
            {
                return false;
            }

            if(otpTransaction.IsUsed)
            {
                return false;
            }

            if(otpTransaction.OneTimePassword != oneTimePassword)
            {
                return false;
            }

            

            otpTransaction.IsUsed = true;

            Context.Update(otpTransaction);
            await Context.SaveChangesAsync();

            return true;

        }
    }
}

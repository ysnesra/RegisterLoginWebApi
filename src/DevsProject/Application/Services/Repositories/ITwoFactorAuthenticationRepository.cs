using Core.Persistence.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface ITwoFactorAuthenticationRepository: IAsyncRepository<TwoFactorAuthenticationTransaction>,IRepository<TwoFactorAuthenticationTransaction>
    {
        public Task<OneTimePasswordDto> CreateOtp(Guid userId, string to, OneTimePasswordChannel channel);
        public Task<bool> VerifyOtp(Guid userId,  string oneTimePassword);
    }
}

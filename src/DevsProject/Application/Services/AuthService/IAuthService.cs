using Core.Infrastructure.Identity;
using Core.Security.Entities;
using Core.Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AuthService
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(AppUser appUser);
        public Task<RefreshToken> CreateRefreshToken(AppUser appUser, string ipAddress);
        public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
    }
}
//AccessToken : Token oluşturan metot
//RefreshToken : Refresh Token oluşturan metot
//AddRefreshToken : Refresh Token ı veritabanına ekleyen metot. Doğrulama karşılaştırma yapmak için

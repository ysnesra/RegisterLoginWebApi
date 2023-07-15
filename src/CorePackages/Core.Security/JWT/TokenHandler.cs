using Core.Security.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT
{
    public class TokenHandler : ITokenHandler
    {
        readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AccessToken CreateAccessToken(int minute, AppUser appUser)
        {
            AccessToken accesstoken = new();

            //Security Key'in simetriğini alma
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            //Şifrelenmiş kimliği oluşturma
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak tokenın ayarları
            accesstoken.Expiration = DateTime.UtcNow.AddSeconds(minute);
            JwtSecurityToken securityToken = new(
                audience: _configuration["Token:Audience"],
                issuer: _configuration["Token:Issuer"],
                expires: accesstoken.Expiration,
                notBefore: DateTime.UtcNow,  //token; üretildiği anda devreye girsin
                signingCredentials: signingCredentials,
                claims: new List<Claim> { new(ClaimTypes.Name, appUser.UserName) }
                );

            //Token oluşturucu sınıfından bir örnek alma:
            JwtSecurityTokenHandler tokenHandler = new();
            accesstoken.Token = tokenHandler.WriteToken(securityToken);

            accesstoken.RefreshToken = CreateRefreshToken();
            return accesstoken;
        }

        public string CreateRefreshToken()
        {
            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}

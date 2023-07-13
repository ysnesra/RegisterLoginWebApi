//using Application.Services.Repositories;
//using Core.Infrastructure.Identity;
//using Core.Persistence.Paging;
//using Core.Security.Entities;
//using Core.Security.JWT;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Application.Services.AuthService
//{
//    public class AuthManager : IAuthService
//    {

//        private readonly UserManager<AppRole> _userManager;
//        private readonly ITokenHelper _tokenHelper;
//        private readonly IRefreshTokenRepository _refreshTokenRepository;

//        public AuthManager(UserManager<AppRole> userManager, ITokenHelper tokenHelper, IRefreshTokenRepository refreshTokenRepository)
//        {
//            _userManager = userManager;
//            _tokenHelper = tokenHelper;
//            _refreshTokenRepository = refreshTokenRepository;
//        }

//        public async Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
//        {
//            RefreshToken addedRefreshToken = await _refreshTokenRepository.AddAsync(refreshToken);
//            return addedRefreshToken;
//        }

//        public async Task<AccessToken> CreateAccessToken(AppUser appUser)
//        {
//            //Kullanıcıyı OperationClaimleri ile birlikte getirir(Include ile)
//            IPaginate<UserOperationClaim> userOperationClaims =
//               await _userManager.GetUserIdAsync(u => u.UserId == appUser.Id,
//                                                                include: u =>
//                                                                    u.Include(u => u.OperationClaim)
//               );
//            //Sadece Id ve Name i getirmesi için Select ile çeke
//            IList<OperationClaim> operationClaims =
//                userOperationClaims.Items.Select(u => new OperationClaim
//                { Id = u.OperationClaim.Id, Name = u.OperationClaim.Name }).ToList();

//            AccessToken accessToken = _tokenHelper.CreateToken(appUser, operationClaims);
//            return accessToken;
//        }

//        public async Task<RefreshToken> CreateRefreshToken(AppUser appUser, string ipAddress)
//        {
//            RefreshToken refreshToken = _tokenHelper.CreateRefreshToken(appUser, ipAddress);
//            return await Task.FromResult(refreshToken);
//        }
//    }
//}
//using Application.Services.Repositories;
//using Core.CrossCuttingConcerns.Exceptions;
//using Core.Infrastructure.Identity;
//using Core.Persistence.Repositories;
//using Core.Security.Entities;
//using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Identity;
//using Persistence.Contexts;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Persistence.Repositories
//{
//    public class RefreshTokenRepository : EfRepositoryBase<RefreshToken, AppIdentityDbContext>, IRefreshTokenRepository
//    {
//        readonly UserManager<AppUser> _userManager;
//        public RefreshTokenRepository(UserManager<AppUser> userManager, AppIdentityDbContext context) : base(context)
//        {
//            _userManager = userManager;
//        }

//        public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser appUser, DateTime accessTokenDate, int addOnAccessTokenDate)
//        {
//            if (appUser != null)
//            {
//                appUser.RefreshToken = refreshToken;
//                appUser.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
//                await _userManager.UpdateAsync(appUser);
//            }
//            else
//                throw new NotFoundUserException();
//        }
//    }
//}

﻿
using Core.Security.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.JWT
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(int minute, AppUser appUser);
        string CreateRefreshToken();
    }
}

using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Dtos
{
    public class OneTimePasswordDto
    {   
        public long OneTimePasswordId { get; set; }   //TwoFactorAuthenticationTransaction tablosundaki Id ye karşılık gelir
        public string OneTimePassword { get; set; }    //TwoFactorAuthenticationTransaction tablosundaki OneTimePassword ye karşılık gelir

    }
}

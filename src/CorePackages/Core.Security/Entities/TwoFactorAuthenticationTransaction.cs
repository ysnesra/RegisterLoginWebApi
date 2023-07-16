using Core.Persistence.Repositories;
using Core.Security.Enums;
using Core.Security.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Entities
{
    public class TwoFactorAuthenticationTransaction : Entity
    {
        /// <summary>
        /// OTP = OneTimePassword
        /// </summary>
        /// 
        // Örnek: 123456 Sms ise TO: 0555 555 55 55, Mail ise To : test@test.com

        public long Id { get; set; } // Primary key
        public Guid UserId { get; set; }  //OTP'yi üreten kullanıcı

        public OneTimePasswordChannel Channel { get; set; } //Otp'nin gönderildiği kanal (sms, mail)

        public string To { get; set; } //OTP'nin gittiği adres.

        public string OneTimePassword { get; set; } //OTP doğrulama kodu

        public bool IsSend { get; set; } //OTP gönderildi mi?  

        public bool IsUsed { get; set; } // Otp kullanıldı mı bilgisi

        public AppUser AppUser { get; set; }


    }   
}

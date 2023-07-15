using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Hashing
{
    public static class PasswordHashingHelper
    {
        public static string EnhancedHashPassword(string plainText)   //Password Hashleme
        {
            if (String.IsNullOrEmpty(plainText))
                throw new ArgumentNullException(nameof(plainText));

            return BCrypt.Net.BCrypt.EnhancedHashPassword(plainText, hashType: HashType.SHA384);
        }

        public static bool EnhancedVerify(string requestPassword, string currentHashedPassword)   //Password Doğrulama
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(requestPassword, currentHashedPassword, hashType: HashType.SHA384);
        }
    }
}

//"BCrypt.Net-Next" librarysi kullanılarak hash algaritması kullanıldı.
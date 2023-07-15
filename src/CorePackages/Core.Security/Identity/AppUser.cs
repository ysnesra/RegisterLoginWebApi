using Core.Security.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.Identity
{
    public class AppUser : IdentityUser<string>
    {
        public string NameSurname { get; set; }

        public ICollection<TwoFactorAuthenticationTransaction> TwoFactorAuthenticationTransactions { get; set; }

    }
}

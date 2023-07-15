using Core.Security.Enums;
using Infrastructure.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstract
{
    public interface IOneTimePasswordService
    {
        public Task<SendOtpResponse> SendOtp(string message, string to, OneTimePasswordChannel channel);
    }
}

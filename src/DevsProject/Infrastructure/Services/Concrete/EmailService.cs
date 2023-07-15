using Core.Security.Enums;
using Infrastructure.Models.Response;
using Infrastructure.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Concrete
{
    public class EmailService : IOneTimePasswordService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<SendOtpResponse> SendOtp(string message, string to, OneTimePasswordChannel channel)
        {
            throw new NotImplementedException();
        }
    }
}

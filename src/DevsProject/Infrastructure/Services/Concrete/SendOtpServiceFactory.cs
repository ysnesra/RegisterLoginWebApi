using Core.Security.Enums;
using Infrastructure.Services.Abstract;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Concrete
{
    public class SendOtpServiceFactory : ISendOtpServiceFactory
    {
        private readonly IConfiguration _configuration;

        public SendOtpServiceFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IOneTimePasswordService CreateSendOtpService(OneTimePasswordChannel channel)
        {
            switch(channel)
            {
                case OneTimePasswordChannel.Email:
                    return new EmailService(_configuration);
                    break;
                case OneTimePasswordChannel.Sms:
                    return new SmsService(_configuration);
                    break;
                    default : throw new ArgumentException();
            }
        }
    }
}

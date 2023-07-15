using Core.Security.Enums;
using Infrastructure.Helpers;
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

        public async Task<SendOtpResponse> SendOtp(string message, List<string> to, OneTimePasswordChannel channel)
        {
            //Müşteriye otomatik olarak görüşme bilgilendirme Maili gönderme
            EmailToInformation emailHelper = new EmailToInformation(_configuration);
            emailHelper.SendEmail(to, message);

            var result = new SendOtpResponse();
            result.Result = true;

            return result;
        }
    }
}

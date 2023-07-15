using Core.Security.Enums;
using Infrastructure.Models.Response;
using Infrastructure.Models.SmsModels;
using Infrastructure.Services.Abstract;
using Infrastructure.Services.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Concrete
{
    public class SmsService : IOneTimePasswordService
    {
        private readonly IConfiguration _configuration;

        public SmsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<SendOtpResponse> SendOtp(string message, string to, OneTimePasswordChannel channel)
        {
            var organikHaberlesme = _configuration.GetSection(nameof(OrganikHaberlesmeModel)).Get<OrganikHaberlesmeModel>();

            var smsOtpRequest = new SmsSendOtp()
            {
                Message = message,
                Recipients = to,
                Header = 100677,
                Type = "sms",
                Encode = "numeric",
                Timeout = 60,
                Length = 6
            };

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Organik-Auth", organikHaberlesme.XOrganikAuth);

            var content = new StringContent(JsonConvert.SerializeObject(smsOtpRequest), Encoding.UTF8, "application/json");

            var url = $"{organikHaberlesme.BaseUrl}sms/otp/send";

            var response = await client.PostAsync(url, content);

            var responseResult = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<SendOtpResponse>(responseResult);

            var requestResult = new SendOtpResponse()
            {
                Data = result.Data,
                Result = result.Result
            };

            return requestResult;


             
        }
    }
}

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


        public async Task<SendOtpResponse> SendOtp(string message, List<string> to, OneTimePasswordChannel channel)
        {
            var organikHaberlesme = _configuration.GetSection(nameof(OrganikHaberlesmeModel)).Get<OrganikHaberlesmeModel>();


            var groups = new List<int>();

            groups.Add(1);
            groups.Add(2);

            var smsOtpRequest = new SmsSendOtp()
            {
                Message = message,
                Groups = groups,
                Recipients = to,
                Commercial = null,
                Type = "sms",
                Otp = false,
                Header = 100677,
                Appeal = false,
                Validity = 48,
                Date = DateTime.UtcNow
            };

          
            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("X-Organik-Auth", organikHaberlesme.XOrganikAuth);

            var data = JsonConvert.SerializeObject(smsOtpRequest).ToLower();

            var content = new StringContent(data, Encoding.UTF8, "application/json");

            var url = $"{organikHaberlesme.BaseUrl}sms/send";

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

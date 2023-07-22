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


            // Organik Haberleşme API'sine yetkilendirme bilgileri eklenir.
            //API isteğine ekstra bir başlık eklemek için DefaultRequestHeaders özelliği kullanılır.
            //Burada "X-Organik-Auth" başlığına organikHaberlesme.XOrganikAuth değeri atanır.Bu, API'ye kimlik doğrulama bilgileri göndermek için kullanılır.

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-Organik-Auth", organikHaberlesme.XOrganikAuth);

            //Apideki değerler Json formatına çevrilir. Küçük harflerle
            var data = JsonConvert.SerializeObject(smsOtpRequest).ToLower();

            //Api verileri Json formatında content nesnesinde tutulur
            var content = new StringContent(data, Encoding.UTF8, "application/json");

            //Organik Haberleşme API'sine gönderilecek URL oluşturulur.
            var url = $"{organikHaberlesme.BaseUrl}sms/send";

            //HTTP POST isteği yapılır.Apiye isteği gönderir
            var response = await client.PostAsync(url, content);

            //API'den gelen responsu okunur.
            var responseResult = await response.Content.ReadAsStringAsync();

            //result ->  JSON formatından SendOtpResponse nesnesine çevrilir.
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

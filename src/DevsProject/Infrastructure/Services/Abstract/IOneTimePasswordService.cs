using Core.Security.Enums;
using Infrastructure.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Abstract
{
    public interface IOneTimePasswordService //SOLID - I (Interface)
    {
        public Task<SendOtpResponse> SendOtp(string message, List<string> to, OneTimePasswordChannel channel);
    }


    /*
     
    request channel
    ....
    ...


    private readonly EmailService _emailservice;
    privere readonly SmsService _smsService;


    if(channel == sms)v                 YANLIŞ YAPI - BAĞIMLILIK
        _smsService.Send(...)
    if(channel == mail)
        _mailservice.Mail(...)



    factory..                          SOLID Mantığı (I ve D)
    factory.SendOtp();
     
     
     */
}

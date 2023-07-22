using Application.Services.Repositories;
using Core.Security.Identity;
using Infrastructure.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Application.TaskScheduler.Jobs
{

    [DisallowConcurrentExecution]   //Job görevini tamamlamazsa bekletiyor
    public class SendOtpJob : IJob
    {
        private readonly ISendOtpServiceFactory _serviceFactory;
        private readonly ITwoFactorAuthenticationRepository _twoFactorAuthenticationRepository;
        private readonly UserManager<AppUser> _userManager;


        public SendOtpJob(ISendOtpServiceFactory serviceFactory, ITwoFactorAuthenticationRepository twoFactorAuthenticationRepository, UserManager<AppUser> userManager)
        {
            _serviceFactory = serviceFactory;
            _twoFactorAuthenticationRepository = twoFactorAuthenticationRepository;
            _userManager = userManager;
        }

        public async Task Execute(IJobExecutionContext context)   
        {
            // 1 132141(userId) Mail 123456(OTP) false(Issend) 

            var otpTransactions = await _twoFactorAuthenticationRepository.Query()
                .Where(_ => _.IsSend == false)
                .ToListAsync();

            var targets = new List<string>(); // Entegrasyondan liste şeklinde istendiği için liste olarak string yazıldı.


            foreach(var otp in otpTransactions)
            {
               var factory = _serviceFactory.CreateSendOtpService(otp.Channel);

                var user = await _userManager.FindByIdAsync(otp.UserId.ToString());

               targets.Add(otp.To);

               //switch(otp.Channel)
               // {
               //     case Core.Security.Enums.OneTimePasswordChannel.Email:
               //         targets.Add(user.Email);
               //         break;
               //     case Core.Security.Enums.OneTimePasswordChannel.Sms:
               //         targets.Add($"9{user.PhoneNumber}");
               //         break;
               //     default: throw new Exception("Tanımsız channel");
               // }

                var sendOtp = factory.SendOtp(otp.OneTimePassword, targets, otp.Channel);

                otp.IsSend = true;

               await _twoFactorAuthenticationRepository.UpdateAsync(otp);
            }

           
        }
    }
}

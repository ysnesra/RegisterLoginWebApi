using Infrastructure.Services.Abstract;
using Infrastructure.Services.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
  
    public static class InsfractureServiceRegistration
    {
        public static IServiceCollection AddInsfractureServices(this IServiceCollection services)
        {

            services.AddTransient<ISendOtpServiceFactory, SendOtpServiceFactory>();
            services.AddTransient<IOneTimePasswordService, SmsService>();
            services.AddTransient<IOneTimePasswordService, EmailService>();
            return services;

        }
    }
}

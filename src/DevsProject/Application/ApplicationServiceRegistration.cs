using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Core.Application.Pipelines.Logging;
using Core.Application.Pipelines.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

           
            services.AddScoped<AuthBusinessRules>();

            //Core.Application daki Pipelinesdaki Validationı çalıştıran clasımızıda(RequestValidationBehavior.cs) devreye sokarız
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped<IAuthService, AuthManager>();
            return services;

        }
    }
}

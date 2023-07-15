using Application.TaskScheduler.Jobs;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class Quartz
    {
        public static IServiceCollection QuartzService(this IServiceCollection services)
        {
            return services
                .AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();

                    var sendOtpJob = new JobKey("sendOtp");
                    q.AddJob<SendOtpJob>(opts => opts.WithIdentity(sendOtpJob));

                    q.AddTrigger(opts => opts
                       .ForJob(sendOtpJob)
                       .WithIdentity("createUser-trigger")
                       .WithCronSchedule("0/5 * * * * ?"));

                }).AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}

using Application.Services.Repositories;
using Core.Infrastructure.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        //Identity configuration: IdentityServer tarafına kurallar koyup veritabanı belirtilir
        public static IServiceCollection AddIdentityServerConfig(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddDbContext<AppIdentityDbContext>(options =>
                                                    options.UseSqlServer(
                                                        configuration.GetConnectionString("DefaultConnection")));

            //User ve Role ile alakalı kısıtlamalar  
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //Email adresi unique olsun
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.User.AllowedUserNameCharacters = "abcçdefghiıjklmnoöpqrsştuüvwxyzABCÇDEFGHIİJKLMNOÖPQRSŞTUÜVWXYZ0123456789-._@+'#!/^%{}*";

            }).AddEntityFrameworkStores<AppIdentityDbContext>()
              .AddDefaultTokenProviders();
           
            return services;
        }


    }
}
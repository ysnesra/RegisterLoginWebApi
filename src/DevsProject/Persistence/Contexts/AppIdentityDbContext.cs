﻿using Core.Security.Entities;
using Core.Security.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class AppIdentityDbContext : IdentityDbContext<AppUser,AppRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {

        }

        public DbSet<TwoFactorAuthenticationTransaction> TwoFactorAuthenticationTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<IdentityRole>().HasData(new IdentityRole { Name ="user" });
            base.OnModelCreating(modelbuilder);
        }
    }
}

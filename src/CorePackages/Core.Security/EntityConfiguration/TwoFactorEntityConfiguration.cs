using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Security.EntityConfiguration
{
    public class TwoFactorEntityConfiguration : IEntityTypeConfiguration<TwoFactorAuthenticationTransaction>
    {
        public void Configure(EntityTypeBuilder<TwoFactorAuthenticationTransaction> builder)
        {
            builder.Property(_ => _.Channel).IsRequired();
            builder.Property(_ => _.To).IsRequired().HasMaxLength(128);
            builder.Property(_ => _.OneTimePassword).IsRequired();
            builder.Property(_=>_.IsSend).IsRequired();
            builder.Property(_ => _.UserId).IsRequired();

            builder
            .HasOne(o => o.AppUser)
            .WithMany(m => m.TwoFactorAuthenticationTransactions)
            .HasForeignKey(fk => fk.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

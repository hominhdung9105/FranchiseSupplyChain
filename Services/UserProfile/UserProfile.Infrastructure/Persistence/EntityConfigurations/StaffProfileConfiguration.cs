using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using UserProfile.Domain.Entities;

namespace UserProfile.Infrastructure.Persistence.EntityConfigurations
{
    public class StaffProfileConfiguration : IEntityTypeConfiguration<StaffProfile>
    {
        public void Configure(EntityTypeBuilder<StaffProfile> builder)
        {
            builder.ToTable("StaffProfiles");

            builder.HasKey(x => x.UserId);

            builder.Property(x => x.Position)
                .HasConversion<string>();

            builder.HasOne(x => x.UserProfile)
                .WithOne(x => x.StaffProfile)
                .HasForeignKey<StaffProfile>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

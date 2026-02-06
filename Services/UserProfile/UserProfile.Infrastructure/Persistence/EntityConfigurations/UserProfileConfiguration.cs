using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserProfile.Infrastructure.Persistence.EntityConfigurations
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<Domain.Entities.Profile>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Profile> builder)
        {
            builder.ToTable("UserProfiles");

            builder.HasKey(x => x.UserId);
            builder.Property(x => x.UserId)
                .ValueGeneratedNever(); // ID comes from IAM

            builder.Property(x => x.FullName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(x => x.UserCategory)
                .HasConversion<string>();

            builder.Property(x => x.Status)
                .HasConversion<string>();
        }
    }
}

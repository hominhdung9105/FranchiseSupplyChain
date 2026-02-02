using IAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Infrastructure.Persistence.EntityConfigurations
{
    public class SecurityPolicyConfiguration : IEntityTypeConfiguration<SecurityPolicy>
    {
        public void Configure(EntityTypeBuilder<SecurityPolicy> builder)
        {
            builder.ToTable("SecurityPolicies");

            builder.HasKey(x => x.Id);
        }
    }

}

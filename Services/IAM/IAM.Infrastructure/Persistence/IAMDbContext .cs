using IAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using System.Text;

namespace IAM.Infrastructure.Persistence
{
    public class IAMDbContext : DbContext
    {
        public IAMDbContext(DbContextOptions<IAMDbContext> options)
            : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
        public DbSet<SecurityPolicy> SecurityPolicies => Set<SecurityPolicy>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IAMDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

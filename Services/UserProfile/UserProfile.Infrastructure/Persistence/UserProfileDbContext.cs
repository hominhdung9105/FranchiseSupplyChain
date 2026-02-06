using Microsoft.EntityFrameworkCore;
using UserProfile.Domain.Entities;

namespace UserProfile.Infrastructure.Persistence
{
    public class UserProfileDbContext : DbContext
    {
        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options)
            : base(options) { }

        public DbSet<Profile> UserProfiles => Set<Profile>();
        public DbSet<StaffProfile> StaffProfiles => Set<StaffProfile>();
        public DbSet<CustomerProfile> CustomerProfiles => Set<CustomerProfile>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserProfileDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using UserProfile.Domain.Entities;

namespace UserProfile.Infrastructure.Persistence
{
    public class UserProfileDbContext : DbContext
    {
        public DbSet<Domain.Entities.UserProfile> UserProfiles { get; set; }
        public DbSet<StaffProfile> StaffProfiles { get; set; }
        public DbSet<CustomerProfile> CustomerProfiles { get; set; }

        public UserProfileDbContext(DbContextOptions<UserProfileDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // UserProfile configuration
            modelBuilder.Entity<Domain.Entities.UserProfile>(entity =>
            {
                entity.ToTable("UserProfiles");
                entity.HasKey(e => e.UserId);
                
                entity.Property(e => e.UserId).ValueGeneratedNever(); // ID comes from IAM
                
                entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                
                entity.Property(e => e.UserCategory).HasConversion<string>();
                entity.Property(e => e.Status).HasConversion<string>();
            });

            // StaffProfile configuration
            modelBuilder.Entity<StaffProfile>(entity =>
            {
                entity.ToTable("StaffProfiles");
                entity.HasKey(e => e.UserId);
                
                entity.Property(e => e.Position).HasConversion<string>();

                entity.HasOne(e => e.UserProfile)
                      .WithOne(e => e.StaffProfile)
                      .HasForeignKey<StaffProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // CustomerProfile configuration
            modelBuilder.Entity<CustomerProfile>(entity =>
            {
                entity.ToTable("CustomerProfiles");
                entity.HasKey(e => e.UserId);

                entity.HasOne(e => e.UserProfile)
                      .WithOne(e => e.CustomerProfile)
                      .HasForeignKey<CustomerProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}

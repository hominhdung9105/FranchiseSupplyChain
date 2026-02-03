using System;
using System.ComponentModel.DataAnnotations;

namespace UserProfile.Domain.Entities
{
    public class UserProfile
    {
        public Guid UserId { get; set; }
        public required string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public UserCategory UserCategory { get; set; }
        public UserStatus Status { get; set; } = UserStatus.ACTIVE;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public StaffProfile? StaffProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
    }

    public enum UserCategory
    {
        CUSTOMER,
        STAFF
    }

    public enum UserStatus
    {
        ACTIVE,
        SUSPENDED
    }
}

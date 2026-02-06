namespace UserProfile.Domain.Entities
{
    public class Profile
    {
        public Guid UserId { get; set; }
        public required string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? UserCategory { get; set; }
        public string? Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public StaffProfile? StaffProfile { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }
    }
}

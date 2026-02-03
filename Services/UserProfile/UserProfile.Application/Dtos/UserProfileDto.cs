namespace UserProfile.Application.Dtos
{
    public sealed class UserProfileDto
    {
        public Guid UserId { get; set; }
        public required string FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public Domain.Entities.UserCategory UserCategory { get; set; }
        public Domain.Entities.UserStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

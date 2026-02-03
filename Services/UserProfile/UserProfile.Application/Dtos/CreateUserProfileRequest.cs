using System.ComponentModel.DataAnnotations;

namespace UserProfile.Application.Dtos
{
    public sealed class CreateUserProfileRequest
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [MaxLength(200)]
        public required string FullName { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public Domain.Entities.UserCategory UserCategory { get; set; }
    }
}

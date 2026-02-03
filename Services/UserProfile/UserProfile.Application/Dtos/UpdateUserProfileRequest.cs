using System.ComponentModel.DataAnnotations;

namespace UserProfile.Application.Dtos
{
    public sealed class UpdateUserProfileRequest
    {
        [Required]
        [MaxLength(200)]
        public required string FullName { get; set; }

        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

        [Required]
        public Domain.Entities.UserStatus Status { get; set; }
    }
}

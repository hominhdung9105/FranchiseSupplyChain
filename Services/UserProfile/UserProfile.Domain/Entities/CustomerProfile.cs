using System;

namespace UserProfile.Domain.Entities
{
    public class CustomerProfile
    {
        public Guid UserId { get; set; }
        public Guid? LoyaltyAccountId { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        // Navigation
        public Profile UserProfile { get; set; } = null!;
    }
}

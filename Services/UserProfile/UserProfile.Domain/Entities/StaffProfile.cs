using System;

namespace UserProfile.Domain.Entities
{
    public class StaffProfile
    {
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public string Position { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public Profile UserProfile { get; set; } = null!;
    }
}

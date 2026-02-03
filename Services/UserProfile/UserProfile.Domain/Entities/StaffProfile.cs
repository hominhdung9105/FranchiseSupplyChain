using System;

namespace UserProfile.Domain.Entities
{
    public class StaffProfile
    {
        public Guid UserId { get; set; }
        public Guid StoreId { get; set; }
        public StaffPosition Position { get; set; }
        public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public UserProfile UserProfile { get; set; } = null!;
    }

    public enum StaffPosition
    {
        STORE_MANAGER,
        SALE_STAFF
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Domain.Entities
{
    public class Permission
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = null!; // ORDER.CREATE
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}

using System;

namespace IAM.Application.Dtos.RoleDtos
{
    public class RoleResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

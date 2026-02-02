using System;

namespace IAM.Application.Dtos.RoleDtos
{
    public class CreateRoleRequestDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}

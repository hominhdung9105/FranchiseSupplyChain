using System;
using System.Collections.Generic;

namespace IAM.Application.Dtos.RoleDtos
{
    public class AssignRolesRequestDto
    {
        public List<Guid> RoleIds { get; set; } = new();
    }
}

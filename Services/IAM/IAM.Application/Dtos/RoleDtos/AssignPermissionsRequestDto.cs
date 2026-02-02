using System;
using System.Collections.Generic;

namespace IAM.Application.Dtos.RoleDtos
{
    public class AssignPermissionsRequestDto
    {
        public List<Guid> PermissionIds { get; set; } = new();
    }
}

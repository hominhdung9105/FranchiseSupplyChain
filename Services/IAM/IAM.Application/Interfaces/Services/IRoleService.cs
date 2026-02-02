using IAM.Application.Dtos.RoleDtos;
using System;
using System.Threading.Tasks;

namespace IAM.Application.Interfaces.Services
{
    public interface IRoleService
    {
        Task<RoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request);
        Task AssignPermissionsAsync(Guid roleId, AssignPermissionsRequestDto request);
    }
}

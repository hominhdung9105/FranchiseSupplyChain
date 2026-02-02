using IAM.Application.Dtos.RoleDtos;
using IAM.Application.Exceptions;
using IAM.Application.Interfaces.Repositories;
using IAM.Application.Interfaces.Services;
using IAM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAM.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<RoleResponseDto> CreateRoleAsync(CreateRoleRequestDto request)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };

            await _roleRepository.AddRoleAsync(role);
            await _roleRepository.SaveChangesAsync();

            return new RoleResponseDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public async Task AssignPermissionsAsync(Guid roleId, AssignPermissionsRequestDto request)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId)
                ?? throw new ApiException(ResponseError.NotFoundRole);

            if (request.PermissionIds.Count == 0)
            {
                return;
            }

            var permissions = await _roleRepository.GetPermissionsByIdsAsync(request.PermissionIds);
            if (permissions.Count != request.PermissionIds.Count)
            {
                throw new ApiException(ResponseError.NotFoundPermission);
            }

            var existingPermissionIds = new HashSet<Guid>(role.RolePermissions.Select(rp => rp.PermissionId));
            foreach (var permission in permissions)
            {
                if (existingPermissionIds.Add(permission.Id))
                {
                    role.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permission.Id
                    });
                }
            }

            await _roleRepository.SaveChangesAsync();
        }
    }
}

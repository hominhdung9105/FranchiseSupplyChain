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
    public class UserService : IUserService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IAuthRepository authRepository, IRoleRepository roleRepository)
        {
            _authRepository = authRepository;
            _roleRepository = roleRepository;
        }

        public async Task ActivateUserAsync(Guid id)
        {
            var user = await _authRepository.GetUserById(id)
                ?? throw new ApiException(ResponseError.NotFoundUser);

            user.Status = "Active";
            user.LockoutEnd = null;
            user.FailedLoginAttempts = 0;
            user.UpdatedAt = DateTime.UtcNow;

            await _authRepository.SaveChangesAsync();
        }

        public async Task DeactivateUserAsync(Guid id)
        {
            var user = await _authRepository.GetUserById(id)
                ?? throw new ApiException(ResponseError.NotFoundUser);

            user.Status = "Inactive";
            user.LockoutEnd = null;
            user.UpdatedAt = DateTime.UtcNow;

            await _authRepository.SaveChangesAsync();
        }

        public async Task LockUserAsync(Guid id)
        {
            var user = await _authRepository.GetUserById(id)
                ?? throw new ApiException(ResponseError.NotFoundUser);

            user.Status = "Locked";
            user.LockoutEnd = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            await _authRepository.SaveChangesAsync();
        }

        public async Task AssignRolesAsync(Guid id, AssignRolesRequestDto request)
        {
            var user = await _authRepository.GetUserById(id)
                ?? throw new ApiException(ResponseError.NotFoundUser);

            if (request.RoleIds.Count == 0)
            {
                return;
            }

            var roles = await _roleRepository.GetRolesByIdsAsync(request.RoleIds);
            if (roles.Count != request.RoleIds.Count)
            {
                throw new ApiException(ResponseError.NotFoundRole);
            }

            var existingRoleIds = new HashSet<Guid>(user.UserRoles.Select(ur => ur.RoleId));
            foreach (var role in roles)
            {
                if (existingRoleIds.Add(role.Id))
                {
                    user.UserRoles.Add(new UserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id
                    });
                }
            }

            user.UpdatedAt = DateTime.UtcNow;
            await _authRepository.SaveChangesAsync();
        }
    }
}

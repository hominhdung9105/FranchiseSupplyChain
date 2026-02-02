using IAM.Application.Dtos.RoleDtos;
using System;
using System.Threading.Tasks;

namespace IAM.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task ActivateUserAsync(Guid id);
        Task DeactivateUserAsync(Guid id);
        Task LockUserAsync(Guid id);
        Task AssignRolesAsync(Guid id, AssignRolesRequestDto request);
    }
}

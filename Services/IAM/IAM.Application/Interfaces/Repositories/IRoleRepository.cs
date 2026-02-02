using IAM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IAM.Application.Interfaces.Repositories
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(Guid id);
        Task<IReadOnlyList<Role>> GetRolesByIdsAsync(IEnumerable<Guid> ids);
        Task<IReadOnlyList<Permission>> GetPermissionsByIdsAsync(IEnumerable<Guid> ids);
        Task SaveChangesAsync();
    }
}

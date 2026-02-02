using IAM.Application.Interfaces.Repositories;
using IAM.Domain.Entities;
using IAM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAM.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IAMDbContext _context;

        public RoleRepository(IAMDbContext context)
        {
            _context = context;
        }

        public async Task AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
        }

        public async Task<Role?> GetRoleByIdAsync(Guid id)
        {
            return await _context.Roles
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<IReadOnlyList<Role>> GetRolesByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Roles
                .Where(r => ids.Contains(r.Id))
                .ToListAsync();
        }

        public async Task<IReadOnlyList<Permission>> GetPermissionsByIdsAsync(IEnumerable<Guid> ids)
        {
            return await _context.Permissions
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

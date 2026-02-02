using IAM.Application.Interfaces.Repositories;
using IAM.Domain.Entities;
using IAM.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IAMDbContext _context;
        public AuthRepository(IAMDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsHaveUserByUsername(string username)
        {
            return await _context.Users.AnyAsync(e => e.Email == username || e.Username == username);
        }

        public async Task<bool> IsHaveUserById(Guid id)
        {
            return await _context.Users.AnyAsync(e => e.Id == id);
        }

        public async Task<User?> GetUserByUsernameOrEmail(string username)
        {
            return await _context.Users
                .Include(e => e.RefreshTokens)
                .FirstOrDefaultAsync(e => e.Email == username || e.Username == username);
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await _context.Users
                .Include(e => e.RefreshTokens)
                .Include(e => e.UserRoles)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task AddToken(RefreshToken refreshToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
        }
        public async Task UpdateToken(RefreshToken refreshToken)
        {
            //await _context.RefreshTokens.Update(refreshToken);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using IAM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> IsHaveUserByUsername(string username);
        Task<bool> IsHaveUserById(Guid id);
        Task<User?> GetUserByUsernameOrEmail(string username);
        Task<User?> GetUserById(Guid id);
        Task<User?> GetUserByEmail(string email);
        Task AddUser(User user);
        Task AddToken(RefreshToken refreshToken);
        Task UpdateToken(RefreshToken refreshToken);
        Task SaveChangesAsync();
    }
}

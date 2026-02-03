using Microsoft.EntityFrameworkCore;
using UserProfile.Application.Interfaces;
using UserProfile.Infrastructure.Persistence;

namespace UserProfile.Infrastructure.Repositories
{
    public sealed class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileDbContext _dbContext;

        public UserProfileRepository(UserProfileDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<Domain.Entities.UserProfile?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            return _dbContext.UserProfiles
                .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
        }

        public async Task<IReadOnlyList<Domain.Entities.UserProfile>> ListAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            if (skip < 0) skip = 0;
            if (take <= 0) take = 20;
            if (take > 200) take = 200;

            return await _dbContext.UserProfiles
                .AsNoTracking()
                .OrderByDescending(x => x.CreatedAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public Task AddAsync(Domain.Entities.UserProfile userProfile, CancellationToken cancellationToken = default)
        {
            return _dbContext.UserProfiles.AddAsync(userProfile, cancellationToken).AsTask();
        }

        public Task<bool> DeleteAsync(Domain.Entities.UserProfile userProfile, CancellationToken cancellationToken = default)
        {
            _dbContext.UserProfiles.Remove(userProfile);
            return Task.FromResult(true);
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

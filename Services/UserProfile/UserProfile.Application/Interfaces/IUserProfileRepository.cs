using UserProfile.Domain.Entities;

namespace UserProfile.Application.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<Domain.Entities.UserProfile?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Domain.Entities.UserProfile>> ListAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task AddAsync(Domain.Entities.UserProfile userProfile, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Domain.Entities.UserProfile userProfile, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

using UserProfile.Domain.Entities;

namespace UserProfile.Application.Interfaces
{
    public interface IUserProfileRepository
    {
        Task<Domain.Entities.Profile?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Domain.Entities.Profile>> ListAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task AddAsync(Domain.Entities.Profile userProfile, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Domain.Entities.Profile userProfile, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

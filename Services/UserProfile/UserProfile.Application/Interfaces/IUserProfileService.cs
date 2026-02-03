using UserProfile.Application.Dtos;

namespace UserProfile.Application.Interfaces
{
    public interface IUserProfileService
    {
        Task<UserProfileDto?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UserProfileDto>> ListAsync(int skip, int take, CancellationToken cancellationToken = default);
        Task<UserProfileDto> CreateAsync(CreateUserProfileRequest request, CancellationToken cancellationToken = default);
        Task<UserProfileDto?> UpdateAsync(Guid userId, UpdateUserProfileRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}

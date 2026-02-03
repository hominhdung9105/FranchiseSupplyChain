using UserProfile.Application.Dtos;
using UserProfile.Application.Interfaces;
using UserProfile.Domain.Entities;

namespace UserProfile.Application.Services
{
    public sealed class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _repository;

        public UserProfileService(IUserProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserProfileDto?> GetByIdAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(userId, cancellationToken);
            return entity is null ? null : MapToDto(entity);
        }

        public async Task<IReadOnlyList<UserProfileDto>> ListAsync(int skip, int take, CancellationToken cancellationToken = default)
        {
            var entities = await _repository.ListAsync(skip, take, cancellationToken);
            return entities.Select(MapToDto).ToList();
        }

        public async Task<UserProfileDto> CreateAsync(CreateUserProfileRequest request, CancellationToken cancellationToken = default)
        {
            var existing = await _repository.GetByIdAsync(request.UserId, cancellationToken);
            if (existing is not null)
            {
                throw new InvalidOperationException($"UserProfile with UserId '{request.UserId}' already exists.");
            }

            var entity = new Domain.Entities.UserProfile
            {
                UserId = request.UserId,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                UserCategory = request.UserCategory,
                Status = UserStatus.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = null
            };

            await _repository.AddAsync(entity, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);

            return MapToDto(entity);
        }

        public async Task<UserProfileDto?> UpdateAsync(Guid userId, UpdateUserProfileRequest request, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(userId, cancellationToken);
            if (entity is null)
            {
                return null;
            }

            entity.FullName = request.FullName;
            entity.PhoneNumber = request.PhoneNumber;
            entity.Status = request.Status;
            entity.UpdatedAt = DateTime.UtcNow;

            await _repository.SaveChangesAsync(cancellationToken);
            return MapToDto(entity);
        }

        public async Task<bool> DeleteAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var entity = await _repository.GetByIdAsync(userId, cancellationToken);
            if (entity is null)
            {
                return false;
            }

            var removed = await _repository.DeleteAsync(entity, cancellationToken);
            if (!removed)
            {
                return false;
            }

            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }

        private static UserProfileDto MapToDto(Domain.Entities.UserProfile entity)
        {
            return new UserProfileDto
            {
                UserId = entity.UserId,
                FullName = entity.FullName,
                PhoneNumber = entity.PhoneNumber,
                UserCategory = entity.UserCategory,
                Status = entity.Status,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt
            };
        }
    }
}

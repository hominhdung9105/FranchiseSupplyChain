using AutoMapper;
using IAM.Application.Dtos.AuthDtos;
using IAM.Application.Exceptions;
using IAM.Application.Interfaces.Repositories;
using IAM.Application.Interfaces.Security;
using IAM.Application.Interfaces.Services;
using IAM.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;

namespace IAM.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AuthService(
            IAuthRepository authRepository,
            ITokenService tokenService,
            IMapper mapper)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<TokenResponseDto> LoginAsync(LoginRequestDto request)
        { 
            var user = await _authRepository.GetUserByUsernameOrEmail(request.Username)
                ?? throw new ApiException(ResponseError.InvalidAccount);

            if (user.Status != "Active") //TODO: Enum
                throw new ApiException(ResponseError.InvalidAccount);

            var verifyResult = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (verifyResult == PasswordVerificationResult.Failed)
                throw new ApiException(ResponseError.InvalidAccount);

            var tokenResponse = IssueToken(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokenResponse.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            await _authRepository.AddToken(refreshToken);
            await _authRepository.SaveChangesAsync();

            return tokenResponse;
        }


        public async Task<TokenResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            // Check existing user by username or email
            var existsByUsernameOrEmail = await _authRepository.IsHaveUserByUsername(request.Username);
                                         
            if (existsByUsernameOrEmail)
            {
                throw new ApiException(ResponseError.UserAlreadyExists);
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Email = request.Email,
                Status = "Active", //TODO: Enum
                FailedLoginAttempts = 0,
                CreatedAt = DateTime.UtcNow
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, request.Password);

            await _authRepository.AddUser(user);
            await _authRepository.SaveChangesAsync();

            var tokenResponse = IssueToken(user);
            var refreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokenResponse.RefreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow,
                UserId = user.Id
            };

            await _authRepository.AddToken(refreshToken);
            await _authRepository.SaveChangesAsync();

            return tokenResponse;
        }

        public async Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var user = await _authRepository.GetUserById(request.Id)
                ?? throw new ApiException(ResponseError.NotFoundUser);

            var existingToken = user.RefreshTokens
                .FirstOrDefault(rt => rt.Token == request.RefreshToken &&
                                      !rt.IsRevoked &&
                                      rt.ExpiresAt > DateTime.UtcNow);

            if (existingToken == null)
            {
                throw new ApiException(ResponseError.InvalidRefreshToken);
            }

            // Overwrite existing valid token instead of creating a new one
            var tokenResponse = IssueToken(user);
            existingToken.Token = tokenResponse.RefreshToken;
            existingToken.ExpiresAt = DateTime.UtcNow.AddDays(7);
            existingToken.IsRevoked = false;
            existingToken.CreatedAt = DateTime.UtcNow;
            await _authRepository.SaveChangesAsync();

            return tokenResponse;
        }

        private TokenResponseDto IssueToken(User user)
        {
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            return new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Id = user.Id.ToString()
            };
        }

        private static string GenerateRefreshToken()
        {
            var bytes = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
        }
    }
}

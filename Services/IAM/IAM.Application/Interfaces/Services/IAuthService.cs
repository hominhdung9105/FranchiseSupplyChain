using IAM.Application.Dtos.AuthDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenResponseDto> LoginAsync(LoginRequestDto request);
        Task<TokenResponseDto> RefreshTokenAsync(RefreshTokenRequestDto request);
        Task<TokenResponseDto> RegisterAsync(RegisterRequestDto request);
        //Task<TokenResponseDto?> GoogleLoginAsync(string email, string name);
    }
}

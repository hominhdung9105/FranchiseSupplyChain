using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Application.Dtos.AuthDtos
{
    public class RefreshTokenRequestDto
    {
        public Guid Id { get; set; }
        public required string RefreshToken { get; set; }
    }
}

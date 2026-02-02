using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Application.Dtos.AuthDtos
{
    public class TokenResponseDto
    {
        public required string AccessToken { get; set; }
        public required string RefreshToken { get; set; }
        public required string Id { get; set; }
        //public required string Role { get; set; }
    }
}

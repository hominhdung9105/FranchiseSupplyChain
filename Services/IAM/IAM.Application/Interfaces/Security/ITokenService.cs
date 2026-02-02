using IAM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Application.Interfaces.Security
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user);
    }

}

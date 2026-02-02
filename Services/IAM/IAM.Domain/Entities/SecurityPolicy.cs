using System;
using System.Collections.Generic;
using System.Text;

namespace IAM.Domain.Entities
{
    public class SecurityPolicy
    {
        public Guid Id { get; set; }

        public int MaxFailedLoginAttempts { get; set; }
        public int LockoutMinutes { get; set; }
        public int PasswordMinLength { get; set; }
    }
}

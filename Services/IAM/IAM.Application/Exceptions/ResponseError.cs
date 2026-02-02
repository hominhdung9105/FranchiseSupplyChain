 namespace IAM.Application.Exceptions
{
    public class ResponseError
    {
        public string Code { get; }
        public string Message { get; }
        public int StatusCode { get; }

        private ResponseError(string code, string message, int statusCode)
        {
            Code = code;
            Message = message;
            StatusCode = statusCode;
        }

        // Authentication Errors
        public static readonly ResponseError InvalidAccount = 
            new("AUTH_001", "Invalid username or password.", 401);

        public static readonly ResponseError InvalidRefreshToken = 
            new("AUTH_002", "Invalid or expired refresh token.", 401);

        public static readonly ResponseError AccountLocked = 
            new("AUTH_003", "Account is locked. Please try again later.", 403);

        public static readonly ResponseError AccountInactive = 
            new("AUTH_004", "Account is inactive.", 403);

        // User Errors
        public static readonly ResponseError NotFoundUser = 
            new("USER_001", "User not found.", 404);

        public static readonly ResponseError UserAlreadyExists = 
            new("USER_002", "User already exists.", 409);

        // Role Errors
        public static readonly ResponseError NotFoundRole =
            new("ROLE_001", "Role not found.", 404);

        public static readonly ResponseError NotFoundPermission =
            new("ROLE_002", "Permission not found.", 404);

        // General Errors
        public static readonly ResponseError Unauthorized = 
            new("GEN_001", "Unauthorized access.", 401);

        public static readonly ResponseError Forbidden = 
            new("GEN_002", "Access forbidden.", 403);

        public static readonly ResponseError NotFound = 
            new("GEN_003", "Resource not found.", 404);

        public static readonly ResponseError InternalServerError = 
            new("GEN_004", "An internal server error occurred.", 500);
    }
}

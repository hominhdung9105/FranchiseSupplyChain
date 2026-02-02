namespace IAM.Application.Exceptions
{
    public class ApiException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public ApiException(ResponseError error)
            : base(error.Message)
        {
            ErrorCode = error.Code;
            StatusCode = error.StatusCode;
        }

        public ApiException(string code, string message, int statusCode = 400)
            : base(message)
        {
            ErrorCode = code;
            StatusCode = statusCode;
        }
    }
}

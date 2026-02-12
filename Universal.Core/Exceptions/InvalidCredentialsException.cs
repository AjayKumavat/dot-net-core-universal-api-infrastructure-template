namespace Universal.Core.Exceptions
{
    public class InvalidCredentialsException : UniversalException
    {
        public InvalidCredentialsException()
        : base(ErrorCode.InvalidCredentials)
        {
        }
    }
}

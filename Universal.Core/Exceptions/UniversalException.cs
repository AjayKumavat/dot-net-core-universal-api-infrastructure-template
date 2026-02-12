namespace Universal.Core.Exceptions
{
    public class UniversalException : Exception
    {
        public ErrorCode ErrorCode { get; }

        protected UniversalException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        protected UniversalException(ErrorCode errorCode, string message)
            : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}

namespace Universal.Core.Exceptions
{
    public class UserNotFoundException : UniversalException
    {
        public UserNotFoundException()
        : base(ErrorCode.UserNotFound)
        {
        }
    }
}
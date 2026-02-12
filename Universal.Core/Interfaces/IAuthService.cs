using Universal.Contracts.Account;

namespace Universal.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginRequestDto loginDto);
    }
}
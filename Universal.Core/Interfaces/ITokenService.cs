using Universal.Core.Entities;

namespace Universal.Core.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
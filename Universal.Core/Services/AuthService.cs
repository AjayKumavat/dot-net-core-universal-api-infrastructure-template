using Universal.Contracts.Account;
using Universal.Core.Exceptions;
using Universal.Core.Interfaces;

namespace Universal.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository,
            IPasswordHasher hasher,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _tokenService = tokenService;
        }
        public async Task<string?> LoginAsync(LoginRequestDto loginDto)
        {
            var user = await _userRepository.GetByEmailAsync(loginDto.Email);

            if (user == null) throw new UserNotFoundException();

            if (!_hasher.Verify(loginDto.Password, user.PasswordHash)) throw new InvalidCredentialsException();

            return _tokenService.GenerateToken(user);
        }
    }
}

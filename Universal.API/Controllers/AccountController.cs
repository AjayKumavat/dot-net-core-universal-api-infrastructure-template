using Microsoft.AspNetCore.Mvc;
using Universal.Contracts.Account;
using Universal.Core.Interfaces;
using Universal.Shared.Responses;

namespace Universal.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AccountController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ApiResponse<string?>> GetToken([FromBody] LoginRequestDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            return Response(token);
        }
    }
}
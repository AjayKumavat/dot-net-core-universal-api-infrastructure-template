using System.ComponentModel.DataAnnotations;

namespace Universal.Contracts.Account
{
    public class LoginRequestDto
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        public required string Password { get; set; }
    }
}
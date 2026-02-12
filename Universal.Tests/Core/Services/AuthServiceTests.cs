using FluentAssertions;
using Moq;
using Universal.Contracts.Account;
using Universal.Core.Entities;
using Universal.Core.Exceptions;
using Universal.Core.Interfaces;
using Universal.Core.Services;

namespace Universal.Tests.Core.Services
{
    public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _passwordHasherMock;
        private readonly Mock<ITokenService> _tokenServiceMock;
        private readonly AuthService _authService;
        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _passwordHasherMock = new Mock<IPasswordHasher>();
            _tokenServiceMock = new Mock<ITokenService>();

            _authService = new AuthService(
                _userRepositoryMock.Object,
                _passwordHasherMock.Object,
                _tokenServiceMock.Object);
        }

        [Fact]
        public async Task LoginAsync_Should_Return_Token_When_Credentials_Are_Valid()
        {
            //Arrange
            var role = new Role("Admin");
            var user = new User("test@test.com", "hashedPassword", role.Id);

            typeof(User)
                .GetProperty("Role")!
                .SetValue(user, role);

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync("test@test.com"))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(x => x.Verify("password123", "hashedPassword"))
                .Returns(true);

            _tokenServiceMock
                .Setup(x => x.GenerateToken(user))
                .Returns("jwt_token");

            //Act
            var result = await _authService.LoginAsync(new LoginRequestDto { 
                Email = "test@test.com",
                Password = "password123"
            });

            //Assert
            result.Should().NotBeNull();
            result.Should().Be("jwt_token");
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_Exception_When_User_Not_Found()
        {
            //Arrange
            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((User?)null);

            //Act
            Func<Task> act = async () => await _authService.LoginAsync(new LoginRequestDto {
                Email = "unknown@test.com",
                Password = "password123"
            });

            //Assert
            await act.Should().ThrowAsync<UserNotFoundException>();
        }

        [Fact]
        public async Task LoginAsync_Should_Throw_Exception_When_Password_Is_Invalid()
        {
            //Arrange
            var role = new Role("Admin");
            var user = new User("test@test.com", "hashedPassword", role.Id);

            typeof(User)
                .GetProperty("Role")!
                .SetValue(user, role);

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(user.Email))
                .ReturnsAsync(user);

            _passwordHasherMock
                .Setup(x => x.Verify("password", "hashedPassword"))
                .Returns(false);

            //Act
            Func<Task> act = async () => await _authService.LoginAsync(new LoginRequestDto
            {
                Email = user.Email,
                Password = "password123"
            });

            //Assert
            await act.Should().ThrowAsync<InvalidCredentialsException>();
        }
    }
}
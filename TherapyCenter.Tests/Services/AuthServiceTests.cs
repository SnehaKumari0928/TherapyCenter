using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using TherapyCenter2.Helper;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Implementations;
using TherapyCenter2.DTOs.Auth;

namespace TherapyCenter.Tests.Services
{
    public class AuthServiceTests
    {

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IJwtHelper> _jwtHelperMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _jwtHelperMock = new Mock<IJwtHelper>();

            _authService = new AuthService(
                _userRepositoryMock.Object,
                _jwtHelperMock.Object
            );
        }


        [Fact]
        public async Task Login_Should_Return_Token_When_Credentials_Are_Valid()
        {
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "123456"
            };

            var user = new User
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Patient"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(loginDto.Email))
                .ReturnsAsync(user);

            _jwtHelperMock
                .Setup(x => x.GenerateToken(It.IsAny<User>()))
                .Returns("fake-jwt-token");

            var result = await _authService.LoginAsync(loginDto);

            result.Should().NotBeNull();
            result!.Token.Should().Be("fake-jwt-token");
        }

        [Fact]
        public async Task Login_Should_Return_Null_When_User_Not_Found()
        {
            var loginDto = new LoginDto
            {
                Email = "notfound@example.com",
                Password = "123456"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(loginDto.Email))
                .ReturnsAsync((User?)null);

            var result = await _authService.LoginAsync(loginDto);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Login_Should_Return_Null_When_Password_Is_Invalid()
        {
            var loginDto = new LoginDto
            {
                Email = "test@example.com",
                Password = "wrongpassword"
            };

            var user = new User
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Patient"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(loginDto.Email))
                .ReturnsAsync(user);

            var result = await _authService.LoginAsync(loginDto);

            result.Should().BeNull();
        }


        [Fact]
        public async Task Register_Should_Create_User_When_Data_Is_Valid()
        {
            var request = new RegisterDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = "123456",
                Role = "Doctor",
                PhoneNumber = "1234567890"
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User?)null);

            _userRepositoryMock
                .Setup(x => x.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync((User u) => u);

            var result = await _authService.RegisterAsync(request);

            result.Should().NotBeNull();
            result.Email.Should().Be(request.Email);
            result.Role.Should().Be(request.Role);
        }

        [Fact]
        public async Task Register_Should_Throw_Exception_When_User_Already_Exists()
        {
            var request = new RegisterDto
            {
                Email = "test@example.com",
                Password = "123456",
                Role = "Doctor"
            };

            var existingUser = new User { Email = request.Email };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync(existingUser);

            Func<Task> act = async () => await _authService.RegisterAsync(request);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("User already exists");
        }

        [Fact]
        public async Task Login_Should_Return_Null_When_Email_Is_Empty()
        {
            var loginDto = new LoginDto { Email = "", Password = "123456" };
            var result = await _authService.LoginAsync(loginDto);
            result.Should().BeNull();
        }

        [Fact]
        public async Task Login_Should_Return_Null_When_Password_Is_Empty()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "" };
            var result = await _authService.LoginAsync(loginDto);
            result.Should().BeNull();
        }

       
        [Fact]
        public async Task Register_Should_Throw_Exception_When_PhoneNumber_Is_Invalid()
        {
            var request = new RegisterDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane@example.com",
                Password = "123456",
                Role = "Doctor",
                PhoneNumber = "abc123" // invalid
            };

            Func<Task> act = async () => await _authService.RegisterAsync(request);
            await act.Should().ThrowAsync<Exception>();
        }


        [Fact]
        public async Task Register_Should_Throw_Exception_When_Role_Is_Invalid()
        {
            var request = new RegisterDto
            {
                Email = "test@example.com",
                Password = "123456",
                Role = "Admin" 
            };

            _userRepositoryMock
                .Setup(x => x.GetByEmailAsync(request.Email))
                .ReturnsAsync((User?)null);

            Func<Task> act = async () => await _authService.RegisterAsync(request);

            await act.Should().ThrowAsync<Exception>()
                .WithMessage("Invalid role selection");
        }

        [Fact]
        public async Task Login_Should_Call_JwtHelper_With_Correct_User()
        {
            var loginDto = new LoginDto { Email = "test@example.com", Password = "123456" };
            var user = new User
            {
                UserId = 1,
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                Role = "Patient"
            };

            _userRepositoryMock.Setup(x => x.GetByEmailAsync(loginDto.Email)).ReturnsAsync(user);
            _jwtHelperMock.Setup(x => x.GenerateToken(It.IsAny<User>())).Returns("token");

            var result = await _authService.LoginAsync(loginDto);

            _jwtHelperMock.Verify(x => x.GenerateToken(user), Times.Once);
        }

        [Fact]
public async Task Register_Should_Hash_Password_Before_Saving()
{
    var request = new RegisterDto
    {
        FirstName = "Alice",
        LastName = "Smith",
        Email = "alice@example.com",
        Password = "plaintextpassword",
        Role = "Doctor"
    };

    _userRepositoryMock.Setup(x => x.GetByEmailAsync(request.Email)).ReturnsAsync((User?)null);
    _userRepositoryMock.Setup(x => x.AddUserAsync(It.IsAny<User>())).ReturnsAsync((User u) => u);

    var result = await _authService.RegisterAsync(request);

    result.Should().NotBeNull();
    result.Email.Should().Be(request.Email);

    _userRepositoryMock.Verify(x => x.AddUserAsync(It.Is<User>(u =>
        BCrypt.Net.BCrypt.Verify("plaintextpassword", u.PasswordHash)
    )), Times.Once);
}
    }
}

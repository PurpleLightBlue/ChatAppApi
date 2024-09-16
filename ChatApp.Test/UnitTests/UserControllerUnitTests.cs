using ChatApp.API.Controllers;
using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace ChatApp.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IUserService> _mockUserService;
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mockUserService = new Mock<IUserService>();
            _mockConfiguration = new Mock<IConfiguration>();
            _controller = new UsersController(_mockUserService.Object, _mockConfiguration.Object);
        }

        [Fact]
        public async Task Register_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };
            _mockUserService.Setup(service => service.RegisterUserAsync(user)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Register(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };
            _mockUserService.Setup(service => service.RegisterUserAsync(user)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.Register(user) as BadRequestObjectResult;

            // Assert

            Assert.NotNull(result); // Ensure the result is not null
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result); // Check it's a BadRequestObjectResult
            Assert.Equal(400, badRequestResult.StatusCode); // Ensure the status code is 400 (Bad Request)
        }

        [Fact]
        public async Task GetProfile_ReturnsOkResult_WithUserProfile()
        {
            // Arrange
            var userName = "testuser";
            var user = new User { Username = userName, Password = "password" };
            _mockUserService.Setup(service => service.GetUserByNameAsync(userName)).ReturnsAsync(user);

            // Act
            var result = await _controller.GetProfile(userName);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<User>(okResult.Value);
            Assert.Equal(userName, returnValue.Username);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithToken_WhenSuccessful()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };
            _mockUserService.Setup(service => service.AuthenticateUserAsync(user.Username, user.Password)).ReturnsAsync(user);
            _mockConfiguration.Setup(config => config["Jwt:Key"]).Returns("supersecretkey124ty6543234dfsgwerverweew126");
            _mockConfiguration.Setup(config => config["Jwt:Issuer"]).Returns("testissuer");
            _mockConfiguration.Setup(config => config["Jwt:Audience"]).Returns("testaudience");

            // Act
            var result = await _controller.Login(user) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result?.StatusCode);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenAuthenticationFails()
        {
            // Arrange
            var user = new User { Username = "testuser", Password = "password" };
            _mockUserService.Setup(service => service.AuthenticateUserAsync(user.Username, user.Password)).ReturnsAsync((User)null);

            // Act
            var result = await _controller.Login(user);

            // Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}

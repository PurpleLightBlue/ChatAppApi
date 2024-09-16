using ChatApp.API.Controllers;
using ChatApp.Domain.Aggregates;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace ChatApp.Tests.Controllers
{
    public class ChatRoomControllerTests
    {
        private readonly Mock<IChatRoomService> _mockChatRoomService;
        private readonly ChatRoomController _controller;

        public ChatRoomControllerTests()
        {
            _mockChatRoomService = new Mock<IChatRoomService>();
            _controller = new ChatRoomController(_mockChatRoomService.Object);
        }

        [Fact]
        public async Task GetJoinableRooms_ReturnsOkResult_WithListOfChatRooms()
        {
            // Arrange
            var chatRooms = new List<ChatRoom> { new ChatRoom("Room1"), new ChatRoom("Room2") };
            _mockChatRoomService.Setup(service => service.GetJoinableRoomsAsync()).ReturnsAsync(chatRooms);

            // Act
            var result = await _controller.GetJoinableRooms();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<ChatRoom>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task JoinRoom_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var chatRoomName = "Room1";
            var userName = "User1";
            _mockChatRoomService.Setup(service => service.JoinRoomAsync(chatRoomName, userName)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.JoinRoom(chatRoomName, userName);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task JoinRoom_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var chatRoomName = "Room1";
            var userName = "User1";
            _mockChatRoomService.Setup(service => service.JoinRoomAsync(chatRoomName, userName)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.JoinRoom(chatRoomName, userName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task LeaveRoom_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var chatRoomName = "Room1";
            var userName = "User1";
            _mockChatRoomService.Setup(service => service.LeaveRoomAsync(chatRoomName, userName)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.LeaveRoom(chatRoomName, userName);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task LeaveRoom_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var chatRoomName = "Room1";
            var userName = "User1";
            _mockChatRoomService.Setup(service => service.LeaveRoomAsync(chatRoomName, userName)).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.LeaveRoom(chatRoomName, userName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }

        [Fact]
        public async Task CreateRoom_ReturnsOkResult_WhenSuccessful()
        {
            // Arrange
            var chatRoomName = "Room1";
            _mockChatRoomService.Setup(service => service.CreateRoomAsync(It.IsAny<ChatRoom>())).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateRoom(chatRoomName);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateRoom_ReturnsBadRequest_WhenExceptionThrown()
        {
            // Arrange
            var chatRoomName = "Room1";
            _mockChatRoomService.Setup(service => service.CreateRoomAsync(It.IsAny<ChatRoom>())).ThrowsAsync(new Exception("Error"));

            // Act
            var result = await _controller.CreateRoom(chatRoomName);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Error", badRequestResult.Value);
        }
    }
}

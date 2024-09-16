using ChatApp.Domain.Aggregates;
using ChatApp.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChatApp.Application.Services
{
    public class ChatRoomService : IChatRoomService
    {
        private readonly IChatRoomRepository _chatRoomRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ChatRoomService> _logger;

        public ChatRoomService(
            IChatRoomRepository chatRoomRepository,
            IUserRepository userRepository,
            ILogger<ChatRoomService> logger)
        {
            _chatRoomRepository = chatRoomRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<ChatRoom>> GetJoinableRoomsAsync()
        {
            return await _chatRoomRepository.GetAllChatRoomsAsync();
        }

        public async Task JoinRoomAsync(string chatRoomName, string userName)
        {
            var chatRoom = await _chatRoomRepository.GetChatRoomByIdAsync(chatRoomName);
            if (chatRoom == null)
            {
                _logger.LogWarning("Invalid chat room ID: {ChatRoomId}", chatRoomName);
                throw new ArgumentException("Invalid chat room ID.");
            }

            var user = await _userRepository.GetUserByNameAsync(userName);
            if (user == null)
            {
                _logger.LogWarning("Invalid user ID: {UserId}", userName);
                throw new ArgumentException("Invalid user ID.");
            }

            if (await _chatRoomRepository.IsUserMemberAsync(chatRoomName, userName))
            {
                _logger.LogWarning("User {UserId} is already a member of chat room {ChatRoomId}.", userName, chatRoomName);
                throw new InvalidOperationException("User is already a member of the chat room.");
            }

            chatRoom.Members.Add(user);
            await _chatRoomRepository.UpdateChatRoomAsync(chatRoom);
        }

        public async Task LeaveRoomAsync(string chatRoomName, string userName)
        {
            var chatRoom = await _chatRoomRepository.GetChatRoomByIdAsync(chatRoomName);
            if (chatRoom == null)
            {
                _logger.LogWarning("Invalid chat room ID: {ChatRoomId}", chatRoomName);
                throw new ArgumentException("Invalid chat room ID.");
            }

            var user = await _userRepository.GetUserByNameAsync(userName);
            if (user == null)
            {
                _logger.LogWarning("Invalid user ID: {UserId}", userName);
                throw new ArgumentException("Invalid user ID.");
            }

            if (!await _chatRoomRepository.IsUserMemberAsync(chatRoomName, userName))
            {
                _logger.LogWarning("User {UserId} is not a member of chat room {ChatRoomId}.", userName, chatRoomName);
                throw new InvalidOperationException("User is not a member of the chat room.");
            }

            chatRoom.Members.Remove(user);
            await _chatRoomRepository.UpdateChatRoomAsync(chatRoom);
        }

        public async Task<bool> IsUserMemberAsync(string chatRoomName, string userName)
        {
            return await _chatRoomRepository.IsUserMemberAsync(chatRoomName, userName);
        }

        public async Task<List<ChatRoom>> GetChatRoomsForUserAsync(string userName)
        {
            return await _chatRoomRepository.GetChatRoomsForUserAsync(userName);
        }

        public async Task<ChatRoom> GetRoomAsync(string roomName)
        {
            return await _chatRoomRepository.GetChatRoomByIdAsync(roomName);
        }

        public async Task CreateRoomAsync(ChatRoom chatRoom) // New method
        {
            await _chatRoomRepository.CreateRoomAsync(chatRoom);
        }
    }
}

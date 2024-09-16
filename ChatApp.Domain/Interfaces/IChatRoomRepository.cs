using ChatApp.Domain.Aggregates;

namespace ChatApp.Domain.Interfaces
{
    public interface IChatRoomRepository
    {
        Task AddChatRoomAsync(ChatRoom chatRoom);
        Task<ChatRoom> GetChatRoomByIdAsync(string chatRoomName);
        Task<List<ChatRoom>> GetAllChatRoomsAsync();
        Task UpdateChatRoomAsync(ChatRoom chatRoom);
        Task<bool> IsUserMemberAsync(string chatRoomName, string userName);
        Task<List<ChatRoom>> GetChatRoomsForUserAsync(string userName);
        Task CreateRoomAsync(ChatRoom chatRoom);
    }
}

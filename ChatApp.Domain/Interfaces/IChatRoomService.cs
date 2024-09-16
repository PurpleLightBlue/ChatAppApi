using ChatApp.Domain.Aggregates;

namespace ChatApp.Domain.Interfaces
{
    public interface IChatRoomService
    {
        Task<IEnumerable<ChatRoom>> GetJoinableRoomsAsync();
        Task<ChatRoom> GetRoomAsync(string roomName);
        Task JoinRoomAsync(string chatRoomName, string userName);
        Task LeaveRoomAsync(string chatRoomName, string userName);
        Task<bool> IsUserMemberAsync(string chatRoomName, string userName);
        Task<List<ChatRoom>> GetChatRoomsForUserAsync(string userName);
        Task CreateRoomAsync(ChatRoom chatRoom);
    }
}

using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
    public interface IMessageService
    {
        Task AddMessageAsync(string content, string senderName, string chatRoomName);
        Task<List<Message>> GetMessagesByChatRoomIdAsync(string chatRoomName);
    }
}
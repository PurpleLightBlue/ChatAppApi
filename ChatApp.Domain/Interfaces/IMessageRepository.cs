using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessageAsync(Message message);
        Task<List<Message>> GetMessagesByChatRoomIdAsync(string chatRoomName);
    }
}

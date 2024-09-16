using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;

namespace ChatApp.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        public async Task AddMessageAsync(string content, string senderName, string chatRoomName)
        {
            var message = new Message(content, senderName, chatRoomName);
            await _messageRepository.AddMessageAsync(message);
        }

        public Task<List<Message>> GetMessagesByChatRoomIdAsync(string chatRoomName)
        {
            return _messageRepository.GetMessagesByChatRoomIdAsync(chatRoomName);
        }
    }

}

using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using ChatApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        // Add a new message to the database
        public async Task AddMessageAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();
        }

        // Retrieve messages by chat room ID
        public async Task<List<Message>> GetMessagesByChatRoomIdAsync(string chatRoomName)
        {
            return await _context.Messages
                .Where(m => m.ChatRoomName == chatRoomName)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}

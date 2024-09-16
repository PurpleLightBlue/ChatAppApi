using ChatApp.Domain.Aggregates;
using ChatApp.Domain.Interfaces;
using ChatApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Infrastructure.Repositories
{
    public class ChatRoomRepository : IChatRoomRepository
    {
        private readonly AppDbContext _context;

        public ChatRoomRepository(AppDbContext context)
        {
            _context = context;
        }

        // Add a new chat room to the database
        public async Task AddChatRoomAsync(ChatRoom chatRoom)
        {
            await _context.ChatRooms.AddAsync(chatRoom);
            await _context.SaveChangesAsync();
        }

        // Retrieve a chat room by ID
        public async Task<ChatRoom> GetChatRoomByIdAsync(string chatRoomName)
        {
            return await _context.ChatRooms
                .Include(c => c.Messages)
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.RoomName == chatRoomName);
        }

        // Retrieve all chat rooms
        public async Task<List<ChatRoom>> GetAllChatRoomsAsync()
        {
            return await _context.ChatRooms
                .Include(c => c.Messages)
                .Include(c => c.Members)
                .ToListAsync();
        }

        public async Task UpdateChatRoomAsync(ChatRoom chatRoom)
        {
            _context.ChatRooms.Update(chatRoom);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserMemberAsync(string chatRoomName, string userName)
        {
            var chatRoom = await _context.ChatRooms
                .Include(c => c.Members)
                .FirstOrDefaultAsync(c => c.RoomName == chatRoomName);

            if (chatRoom == null)
            {
                return false;
            }

            return chatRoom.Members.Any(m => m.Username == userName);
        }

        public async Task<List<ChatRoom>> GetChatRoomsForUserAsync(string userName)
        {
            var chatRooms = await _context.ChatRooms
                .Where(c => c.Members.Any(m => m.Username == userName))
                .ToListAsync();

            return chatRooms;
        }

        public async Task CreateRoomAsync(ChatRoom chatRoom) // New method
        {
            _context.ChatRooms.Add(chatRoom);
            await _context.SaveChangesAsync();
        }
    }
}

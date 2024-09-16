using ChatApp.Domain.Entities;

namespace ChatApp.Domain.Aggregates
{
    public class ChatRoom
    {
        public string RoomName { get; private set; }
        public List<Message> Messages { get; private set; }
        public List<User> Members { get; private set; } // List of members in the chat room

        public ChatRoom(string roomName)
        {
            RoomName = roomName;
            Messages = new List<Message>();
            Members = new List<User>();
        }
    }
}

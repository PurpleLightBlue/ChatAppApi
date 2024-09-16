using ChatApp.Domain.Aggregates;

namespace ChatApp.Domain.Entities
{
    public class Message
    {
        public Guid Id { get; private set; } // Unique identifier for each message
        public string Content { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string SenderName { get; private set; } // Reference to the user who sent the message
        public User Sender { get; set; } // Navigation property
        public string ChatRoomName { get; private set; } // Reference to the ChatRoom this message belongs to
        public ChatRoom ChatRoom { get; set; } // Navigation property

        // Constructor for creating a new message
        public Message(string content, string senderName, string chatRoomName)
        {
            Id = Guid.NewGuid();
            Content = content;
            SenderName = senderName;
            ChatRoomName = chatRoomName;
            Timestamp = DateTime.UtcNow;
        }
    }

}

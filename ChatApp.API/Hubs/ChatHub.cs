using ChatApp.Domain.Entities;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace ChatApp.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly IChatRoomService _chatRoomService;
        private readonly IUserService _userService;

        public ChatHub(IMessageService messageService, IChatRoomService chatRoomService, IUserService userService)
        {
            _messageService = messageService;
            _chatRoomService = chatRoomService;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            if (user != null)
            {
                var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var rooms = await _chatRoomService.GetChatRoomsForUserAsync(userName);
                // Add to each assigned group.
                foreach (var item in rooms)
                {
                    Groups.AddToGroupAsync(Context.ConnectionId, item.RoomName);
                }
            }

            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string content, string chatRoomName)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new ArgumentException($"'{nameof(content)}' cannot be null or whitespace.", nameof(content));
            }

            if (string.IsNullOrWhiteSpace(chatRoomName))
            {
                throw new ArgumentException($"'{nameof(chatRoomName)}' cannot be null or whitespace.", nameof(chatRoomName));
            }

            var user = Context.User;
            if (user != null)
            {
                var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Check if the sender is a member of the chat room
                if (!await _chatRoomService.IsUserMemberAsync(chatRoomName, userName))
                {
                    //await Clients.Caller.SendAsync("Error", "You are not a member of this chat room.");
                    await Clients.Group(chatRoomName).SendAsync("Error", "You are not a member of this chat room.");
                    return;
                }


                // Add the message
                await _messageService.AddMessageAsync(content, userName, chatRoomName);

                // Broadcast the message to the chat room
                await Clients.Group(chatRoomName).SendAsync("ReceiveMessage", content, userName);
            }
        }


        public async Task AddToRoomAsync(string chatRoomName)
        {
            if (string.IsNullOrWhiteSpace(chatRoomName))
            {
                throw new ArgumentException($"'{nameof(chatRoomName)}' cannot be null or whitespace.", nameof(chatRoomName));
            }

            var user = Context.User;
            if (user != null)
            {
                var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                // Retrieve room.
                var room = _chatRoomService.GetRoomAsync(chatRoomName);

                if (room != null)
                {
                    await _chatRoomService.JoinRoomAsync(chatRoomName, userName);
                    Groups.AddToGroupAsync(Context.ConnectionId, chatRoomName);
                }
            }
            else
            {
                throw new Exception("No user context found");
            }
        }

        public async Task RemoveFromRoomAsync(string chatRoomName)
        {
            if (string.IsNullOrWhiteSpace(chatRoomName))
            {
                throw new ArgumentException($"'{nameof(chatRoomName)}' cannot be null or whitespace.", nameof(chatRoomName));
            }

            var user = Context.User;
            if (user != null)
            {
                var userName = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var room = _chatRoomService.GetRoomAsync(chatRoomName);
                if (room != null)
                {
                    await _chatRoomService.LeaveRoomAsync(chatRoomName, userName);
                    Groups.RemoveFromGroupAsync(Context.ConnectionId, chatRoomName);
                }
            }
            else
            {
                throw new Exception("No user context found");
            }
        }

        public async Task<List<Message>> GetChatTranscript(string chatRoomName)
        {
            if (string.IsNullOrWhiteSpace(chatRoomName))
            {
                throw new ArgumentException($"'{nameof(chatRoomName)}' cannot be null or whitespace.", nameof(chatRoomName));
            }

            var results = await _messageService.GetMessagesByChatRoomIdAsync(chatRoomName);
            return results;
        }

    }
}

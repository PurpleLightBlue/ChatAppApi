using ChatApp.Domain.Aggregates;
using ChatApp.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChatRoomController : ControllerBase
    {
        private readonly IChatRoomService _chatRoomService;

        public ChatRoomController(IChatRoomService chatRoomService)
        {
            _chatRoomService = chatRoomService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChatRoom>>> GetJoinableRooms()
        {
            try
            {
                var rooms = await _chatRoomService.GetJoinableRoomsAsync();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{chatRoomId}/join")]
        public async Task<ActionResult> JoinRoom(string chatRoomName, [FromBody] string userName)
        {
            try
            {
                await _chatRoomService.JoinRoomAsync(chatRoomName, userName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{chatRoomId}/leave")]
        public async Task<ActionResult> LeaveRoom(string chatRoomName, [FromBody] string userName)
        {
            try
            {
                await _chatRoomService.LeaveRoomAsync(chatRoomName, userName);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateRoom([FromBody] string chatRoomName)
        {
            try
            {
                var chatRoom = new ChatRoom(chatRoomName);
                await _chatRoomService.CreateRoomAsync(chatRoom);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

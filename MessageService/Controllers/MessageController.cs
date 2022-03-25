using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.BLL.RepositoryInterfaces;
using MessageService.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;

        public MessageController(IMessageRepository messageRepository, IChatRepository chatrepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatrepository;
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
                ServiceResponse<Guid> response = await _messageRepository.SendMessage(messageDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("GetChatsByUserID")]
        public async Task<IActionResult> GetChatsByUserID(Guid UserID)
        {
            try
            {
                List<Chat> chats = await _chatRepository.GetAllByUserID(UserID);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

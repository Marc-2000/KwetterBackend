using MessageService.BLL.DTOs;
using MessageService.BLL.Models;
using MessageService.BLL.RepositoryInterfaces;
using MessageService.DAL.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MessageService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;
        }

        [Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
                ServiceResponse<Message> response = await _messageRepository.SendMessage(messageDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

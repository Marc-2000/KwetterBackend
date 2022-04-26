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
        private readonly ILogger<MessageController> _logger;

        public MessageController(IMessageRepository messageRepository, ILogger<MessageController> logger)
        {
            _messageRepository = messageRepository;
            _logger = logger;
        }

        //[Authorize]
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            try
            {
                _logger.LogInformation("Creating new message", messageDTO);
                ServiceResponse<Message> response = await _messageRepository.SendMessage(messageDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        //getmessagesbychatid
    }
}

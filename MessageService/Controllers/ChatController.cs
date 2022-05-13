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
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatrepository)
        {
            _chatRepository = chatrepository;
        }

        //[Authorize]
        [HttpPost("CreateChat")]
        public async Task<IActionResult> CreateChat(ChatDTO chatDTO)
        {
            try
            {
                ServiceResponse<Chat> response = await _chatRepository.CreateChat(chatDTO);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize]
        [HttpGet("GetChatsByUserID")]
        public async Task<IActionResult> GetChatsByUserId(Guid userId)
        {
            try
            {
                List<Chat> chats = await _chatRepository.GetAllByUserID(userId);
                return Ok(chats);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        //[Authorize]
        [HttpDelete("DeleteChat")]
        public async Task<IActionResult> DeleteChat(Guid chatId)
        {
            try
            {
                ServiceResponse<Chat> response = await _chatRepository.DeleteChat(chatId);
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

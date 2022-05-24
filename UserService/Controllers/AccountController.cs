using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Models;
using UserService.BLL.DTOs;
using UserService.Entities;
using UserService.BLL.RepositoryInterfaces;
using UserService.DAL.Context;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IPublishEndpoint publishEndpoint;

        public AccountController(IAccountRepository accountRepository, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                ServiceResponse response = await _accountRepository.Register(user);
                if (response.Success == true)
                {
                    await publishEndpoint.Publish(new QueueMessage<SharedUser>
                    {
                        Data = new SharedUser { Id = response.Id, Username = response.Username},
                        Type = QueueMessageType.Insert
                    });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO user)
        {
            try
            {
                ServiceResponse response = await _accountRepository.Login(user);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete([FromBody] Guid userId)
        {
            try
            {
                ServiceResponse response = await _accountRepository.Delete(userId);
                if (response.Success == true)
                {
                    await publishEndpoint.Publish(new QueueMessage<SharedUser>
                    {
                        Data = new SharedUser { Id = userId },
                        Type = QueueMessageType.Delete
                    });
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [Authorize]
        [HttpGet("byID/{id}")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            try
            {
                User user = await _accountRepository.GetByID(Guid.Parse(id));
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

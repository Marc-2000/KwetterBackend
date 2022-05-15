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
        private readonly IBus _busService;
        private IPublishEndpoint publishEndpoint;

        public AccountController(IAccountRepository accountRepository, IBus busService, IPublishEndpoint publishEndpoint)
        {
            _accountRepository = accountRepository;
            _busService = busService;
            this.publishEndpoint = publishEndpoint;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDTO user)
        {
            try
            {
                ServiceResponse response = await _accountRepository.Register(user);
                if (response != null)
                {
                    Uri uri = new("rabbitmq://localhost/userQueue");
                    var endPoint = await _busService.GetSendEndpoint(uri);
                    await endPoint.Send(response);
                }
                Console.WriteLine(response);
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

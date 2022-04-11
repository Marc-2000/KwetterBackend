using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.BLL.DTOs;
using TweetService.BLL.RepositoryInterfaces;
using TweetService.DAL.Context;
using TweetService.BLL.Models;

namespace TweetService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TweetController : ControllerBase
    {
        private readonly ITweetRepository _tweetRepository;

        public TweetController(ITweetRepository tweetRepository)
        {
            _tweetRepository = tweetRepository;
        }

        [HttpPost("PostTweet")]
        public async Task<IActionResult> PostTweet([FromBody] TweetDTO tweet)
        {
            try
            {
                ServiceResponse<Tweet> response = await _tweetRepository.PostTweet(tweet);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteTweet")]
        public async Task<IActionResult> DeleteTweet([FromBody] Guid tweetId)
        {
            try
            {
                ServiceResponse<Tweet> response = await _tweetRepository.DeleteTweet(tweetId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetTweets")]
        public async Task<IActionResult> GetTweets()
        {
            try
            {
                List<Tweet> tweets = await _tweetRepository.GetTweets();
                return Ok(tweets);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}

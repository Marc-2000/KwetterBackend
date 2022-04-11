using TweetService.BLL.DTOs;
using TweetService.DAL.Context;
using TweetService.BLL.Models;

namespace TweetService.BLL.RepositoryInterfaces
{
    public interface ITweetRepository
    {
        public Task<ServiceResponse<Tweet>> PostTweet(TweetDTO tweet);
        public Task<ServiceResponse<Tweet>> DeleteTweet(Guid tweetId);
        public Task<List<Tweet>> GetTweets();
    }
}

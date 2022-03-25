using Microsoft.EntityFrameworkCore;
using TweetService.BLL.DTOs;
using TweetService.BLL.RepositoryInterfaces;
using TweetService.DAL.Context;
using TweetService.BLL.Models;

namespace TweetService.BLL.Repositories
{
    public class TweetRepository : ITweetRepository
    {
        private readonly DataContext _context;

        public TweetRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<Tweet>> PostTweet(TweetDTO tweet)
        {
            // Create new empty response
            ServiceResponse<Tweet> response = new();

            Tweet newTweet = new()
            {
                Text = tweet.Text,
                DateTime = tweet.DateTime,
                UserID = tweet.UserID,
                Likes = 0,
                Retweets = 0,
            };

            await _context.Tweets.AddAsync(newTweet);
            await _context.SaveChangesAsync();

            //set return data
            response.Message = "Tweet created succesfully.";
            return response;
        }

        public async Task<ServiceResponse<Tweet>> DeleteTweet(Guid TweetID)
        {
            return null;
        }

        public async Task<List<Tweet>> GetTweets() //Timerange?
        {
            var entitiesToReturn = await _context.Tweets.ToListAsync();
            return entitiesToReturn;
        }
    }
}

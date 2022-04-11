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
            ServiceResponse<Tweet> response = new();

            try
            {
                Tweet newTweet = new()
                {
                    ID = Guid.NewGuid(),
                    Text = tweet.Text,
                    DateTime = DateTime.Now,
                    UserID = tweet.UserID,
                    Likes = 0,
                    Retweets = 0,
                };

                _context.Tweets.Add(newTweet);
                await _context.SaveChangesAsync();

                CheckForHashtags(newTweet);
                CheckForMentions(newTweet);

                response.Message = "Tweet created succesfully.";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return response.BadResponse("Something went wrong. Please reload the page and try again.");
            }
        }

        public async Task<ServiceResponse<Tweet>> DeleteTweet(Guid tweetId)
        {
            ServiceResponse<Tweet> response = new();

            try
            {
                Tweet tweet = await _context.Tweets.FirstOrDefaultAsync(x => x.ID == tweetId);

                if (tweet == null)
                {
                    return response.BadResponse("Tweet does not exist, or is already deleted!");
                }

                TaggedUser taggedUsers = await _context.TaggedUsers.FirstOrDefaultAsync(x => x.TweetID == tweetId);

                _context.Tweets.Remove(tweet);
                _context.TaggedUsers.Remove(taggedUsers);
                await _context.SaveChangesAsync();

                response.Message = "Tweet has been removed!";
                response.Success = true;
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return response.BadResponse("Something went wrong. Please reload the page and try again.");
            }
        }

        public async Task<List<Tweet>> GetTweets() //Timerange?
        {
            var entitiesToReturn = await _context.Tweets.ToListAsync();
            return entitiesToReturn;
        }

        public void CheckForHashtags(Tweet tweet)
        {
            string[] words = tweet.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            List<string> hashtags = new();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("#"))
                {
                    hashtags.Add(words[i]);
                }
            }

            for (int i = 0; i < hashtags.Count; i++)
            {
                Hashtag hashtagFromDb = _context.Hashtags.FirstOrDefault(x => x.Title == hashtags[i]);

                if (hashtagFromDb == null)
                {
                    Hashtag newHashtag = new Hashtag()
                    {
                        Title = hashtags[i]
                    };

                    TweetHashtag newTweetHashtag = new TweetHashtag()
                    {
                        TweetId = tweet.ID,
                        Tweet = tweet,
                        HashtagId = newHashtag.Id,
                        Hashtag = newHashtag
                    };

                    _context.Hashtags.Add(newHashtag);
                    _context.TweetHashtags.Add(newTweetHashtag);
                    _context.SaveChanges();
                }
                else
                {
                    TweetHashtag newTweetHashtag = new TweetHashtag()
                    {
                        TweetId = tweet.ID,
                        Tweet = tweet,
                        HashtagId = hashtagFromDb.Id,
                        Hashtag = hashtagFromDb
                    };

                    _context.TweetHashtags.Add(newTweetHashtag);
                    _context.SaveChanges();
                }
            }
        }

        public void CheckForMentions(Tweet tweet)
        {
            string[] words = tweet.Text.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            List<string> mentions = new();

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].StartsWith("@"))
                {
                    mentions.Add(words[i]);
                }
            }

            for (int i = 0; i < mentions.Count; i++)
            {
                string username = mentions[i].Replace("@", string.Empty);

                User userFromDb = _context.Users.FirstOrDefault(x => x.Username == username);

                if (userFromDb != null)
                {
                    TaggedUser taggedUsers = new()
                    {
                        UserID = userFromDb.ID,
                        TweetID = tweet.ID
                    };

                    _context.TaggedUsers.Add(taggedUsers);
                    _context.SaveChangesAsync();
                }
            }
        }
    }
}

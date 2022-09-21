using Card.api.Data;
using Card.api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Repository
{
    public class TweetRepository:ITweetRepository
    {
        public readonly UserContext _context;
        public TweetRepository(UserContext context)
        {
            this._context = context;
        }
        public async Task<List<User>> GetAllUser()
        {
            var records = await _context.Users.Select(x => new User()
            {
                Name = x.Name,
                UserId = x.UserId,
                Email = x.Email,
                Password = x.Password
            }).ToListAsync();

            return records;
        }

        public async Task<List<Tweet>> GetAllTweet()
        {
            var records = await _context.Tweets.Select(x => new Tweet()
            {
                UserId = x.UserId,
                TweetId = x.TweetId,
                post = x.post
            }).ToListAsync();

            return records;
        }

        public async Task<List<Tweet>> GetTweetById(int id)
        {
            var records = await _context.Tweets.Where(x=> x.UserId == id).Select(x => new Tweet()
            {
                UserId = x.UserId,
                TweetId = x.TweetId,
                post = x.post
            }).ToListAsync();

            return records;
        }

        public async Task<int> AddTweet(Tweet tweet)
        {
            var tweetdata = new Tweet()
            {
                UserId = tweet.UserId,
                post = tweet.post
            };
            _context.Tweets.Add(tweetdata);
            await _context.SaveChangesAsync();

            return tweetdata.TweetId;
        }

        public async Task UpdateTweet(int tweetId,Tweet tweet)
        {
            //var tweetdata = await _context.Tweets.FindAsync(tweetId);
            //if(tweetdata != null)
            //{
            //    tweetdata.post = tweet.post;
            //    await _context.SaveChangesAsync();
            //}
            var tweetdata = new Tweet()
            {
                TweetId = tweet.TweetId,
                UserId = tweet.UserId,
                post = tweet.post
            };
            _context.Tweets.Update(tweetdata);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTweet(int id)
        {
            var tweetdata = new Tweet() { TweetId = id };
            _context.Tweets.Remove(tweetdata);
            await _context.SaveChangesAsync();
        }

        public async Task<int> AddComment(Comment comment)
        {
            var commentdata = new Comment()
            {
                UserId = comment.UserId,
                TweetId = comment.TweetId,
                Reply = comment.Reply
            };
            _context.Comments.Add(commentdata);
            await _context.SaveChangesAsync();

            return comment.TweetId;
        }

        public async Task<List<Comment>> GetAllComment()
        {
            var records = await _context.Comments.Select(x => new Comment()
            {
                UserId = x.UserId,
                TweetId = x.TweetId,
                ReplyId = x.ReplyId,
                Reply = x.Reply
            }).ToListAsync();

            return records;
        }
        public async Task<int> AddLike(Like like)
        {
            var likedata = new Like()
            {
                //LikeCount = like.LikeCount,
                UserId = like.UserId,
                TweetId = like.TweetId
                
            };
            _context.Likes.Add(likedata);
            await _context.SaveChangesAsync();

            return like.TweetId;
        }
        public async Task<List<Like>> GetAllLike()
        {
            var records = await _context.Likes.Select(x => new Like()
            {
                UserId = x.UserId,
                TweetId = x.TweetId,
                LikeId = x.LikeId,
                LikeCount = x.LikeCount
            }).ToListAsync();

            return records;
        }
    }
}

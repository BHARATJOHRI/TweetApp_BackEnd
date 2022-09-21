using Card.api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Repository
{
    public interface ITweetRepository
    {
        public Task<List<User>> GetAllUser();
        public Task<List<Tweet>> GetAllTweet();
        Task<List<Tweet>> GetTweetById(int id);
        Task<int> AddTweet(Tweet tweet);
        Task UpdateTweet(int tweetId, Tweet tweet);
        Task DeleteTweet(int id);
        Task<int> AddComment(Comment comment);
        Task<List<Comment>> GetAllComment();
        Task<int> AddLike(Like like);
        Task<List<Like>> GetAllLike();
    }
}

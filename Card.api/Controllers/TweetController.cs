using Card.api.Data;
using Card.api.Models;
using Card.api.RabitMQ;
using Card.api.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TweetController : ControllerBase
    {
        private readonly ITweetRepository _tweetRepository;
        public readonly UserContext _contextdata;
        private readonly IRabitMQProducer _rabitMQProducer;
        public TweetController(ITweetRepository tweetRepository, UserContext context, IRabitMQProducer rabitMQProducer)
        {
            _tweetRepository = tweetRepository;
            _contextdata = context;
            _rabitMQProducer = rabitMQProducer;
        }

      
        [HttpGet("user")]
        public async Task<IActionResult> GetAllUser()
        {
            var records = await _tweetRepository.GetAllUser();
            return Ok(records);
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTweet()
        {
            var records = await _tweetRepository.GetAllTweet();
            return Ok(records);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTweetById([FromRoute] int id)
        {
            var records = await _tweetRepository.GetTweetById(id);
            if(records == null)
            {
                return NotFound();
            }
            return Ok(records);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewTweet([FromBody]Tweet tweet)
        {
            var records = await _tweetRepository.AddTweet(tweet);
            _rabitMQProducer.SendTweetMessage(tweet);
            return CreatedAtAction(nameof(GetTweetById),new { id = tweet.UserId, controller = "tweet" },tweet.TweetId);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTweet([FromBody] Tweet tweet,[FromRoute] int id)
        {
            await _tweetRepository.UpdateTweet(id,tweet);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTweet([FromRoute] int id)
        {
            await _tweetRepository.DeleteTweet(id);
            return Ok();
        }
        [HttpGet("comment")]
        public async Task<IActionResult> GetAllComment()
        {
            var records = await _tweetRepository.GetAllComment();
            return Ok(records);
        }
        [HttpPost("comment")]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            var records = await _tweetRepository.AddComment(comment);
            return CreatedAtAction(nameof(GetTweetById), new { id = comment.UserId, controller = "tweet" }, comment.ReplyId);
        }
        [HttpGet("like")]
        public async Task<IActionResult> GetAllLike()
        {
            var records = await _tweetRepository.GetAllLike();
            return Ok(records);
        }
        [HttpPost("like")]
        public async Task<IActionResult> AddLike([FromBody] Like like)
        {
            var records = await _tweetRepository.AddLike(like);
            //return CreatedAtAction(nameof(GetTweetById), new { id = like.UserId, controller = "tweet" }, like.LikeId);
            return Ok();
        }
        //[HttpPost("likec")]
        //public IEnumerable<IActionResult> Like()
        //{
        //    //var records = await _contextdata.Likes.GroupBy(x => x.TweetId).Select(x => new 
        //    //{
        //    //    TweetId = x.Key,
        //    //    LikeCount = x.Count()

        //    //});
        //    var records =  _contextdata.Likes.GroupBy(n => new { n.TweetId}).Select
        //        (g => new { g.Key.TweetId}).ToList();
        //    return (IEnumerable<IActionResult>)records;
        //}
    }
}

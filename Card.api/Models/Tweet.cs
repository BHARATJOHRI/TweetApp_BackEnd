using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Models
{
    public class Tweet
    {
        [Key]
        public int TweetId { get; set; }
        public int UserId { get; set; }
        public string post { get; set; }
    }
}

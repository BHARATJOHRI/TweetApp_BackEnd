using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.Models
{
    public class Like
    {
        [Key]
        public int LikeId { get; set; }
        public int TweetId { get; set; }
        public int UserId { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int LikeCount { get; set; }
    }
}

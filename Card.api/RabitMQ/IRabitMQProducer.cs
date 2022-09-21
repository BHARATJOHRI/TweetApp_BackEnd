using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Card.api.RabitMQ
{
    public interface IRabitMQProducer
    {
        public void SendTweetMessage<T>(T message);
    }
}

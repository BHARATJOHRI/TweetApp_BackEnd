using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace RabbitMQTweet.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            //Create the RabbitMQ connection using connection factory details as i mentioned above
            var connection = factory.CreateConnection();
            //Here we create channel with session and model
            using
            var channel = connection.CreateModel();
            //declare the queue after mentioning name and a few property related to that
            channel.QueueDeclare("tweet", exclusive: false);
            //Set Event object which listen message from chanel which is sent by producer
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine("Product message received: {0}", message);
            };
            //read the message
            channel.BasicConsume(queue: "tweet", autoAck: true, consumer: consumer);
            Console.ReadKey();
        }
    }
}

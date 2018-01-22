using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Publisher.Publisher
{
    public class Publisher : IPublisher
    {
        private IConnection _connection;
        private IConnectionFactory _connectionFactory;

        private const string _exchangeName = "sock-inbound";
        private const string _queueName = "sock-inbound-queue";
        private const string _routingKey = "sock.firemessage";

        public Publisher(IConnection connection)
        {
            _connection = connection;
            CreateQueueAndExchange();
        }


        private bool CreateQueueAndExchange()
        {
            var exchangeName = _exchangeName;
            var queueName = _queueName;
            var rountingKey = _routingKey;

            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, true);

                //var args = new Dictionary<string, object>();
                //args.Add("x-message-ttl", 80000);
                channel.QueueDeclare(queueName, true, false, false, null);
                channel.QueueBind(queueName, exchangeName, rountingKey);
            }

            return true;
        }

        public int PublishMessages(int count)
        {
            int countRun = count;
            var playerModel = new PlayerModel();

            using (var channel = _connection.CreateModel())
            {
                for (int i = 1; i <= countRun; i++)
                {
                    int incrementWager = 1500;
                    var random = new Random().Next(0, 10000);
                    incrementWager = incrementWager + random;
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));

                    if (i % 2 == 0)
                    {
                        playerModel.UserId = 1;
                        playerModel.TotalWagers = incrementWager;
                        playerModel.Name = "Tom";
                    }
                    else if (i % 3 == 0)
                    {
                        playerModel.UserId = 3;
                        playerModel.TotalWagers = incrementWager;
                        playerModel.Name = "David";
                    }
                    else
                    {
                        playerModel.UserId = 7;
                        playerModel.TotalWagers = incrementWager;
                        playerModel.Name = "Harly";
                    }

                    var message = Newtonsoft.Json.JsonConvert.SerializeObject(playerModel);
                    Console.WriteLine(message);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: _exchangeName,
                                         routingKey: _routingKey,
                                         basicProperties: null,
                                         body: body);
                    count++;
                }
            }

            return countRun;
        }
    }
}

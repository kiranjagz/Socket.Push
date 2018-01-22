using Rabbit.Publisher.Publisher;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rabbit.Publisher
{
    class Program
    {
        private static IConnectionFactory _connectionFactory;
        private static IConnection _connection;
        private static IPublisher _publisher;

        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Press any key to start, and 'exit' to escape");
                String data = Console.ReadLine();
                if (data.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

                var publishCount = 25;
                _connectionFactory = new ConnectionFactory { HostName = "localhost" };
                _connection = _connectionFactory.CreateConnection();
                _publisher = new Publisher.Publisher(_connection);

                var count = _publisher.PublishMessages(publishCount);
                Console.WriteLine($"Number of messsages sent to the irongate. {count}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops ze daizy, le system, shes not working, please contact someone who actually cares :): {ex.Message}");
            }

            Console.Read();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Subscriber.Settings
{
    public class Setting : ISetting
    {
        public string HostName { get; private set; } = "localhost";
        public string ExchangeName { get; private set; } = "sock-inbound";
        public string RoutingKey { get; private set; } = "sock.firemessage";
        public string QueueName { get; private set; } = "sock-inbound-queue";
    }
}

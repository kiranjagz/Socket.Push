using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Subscriber.Settings
{
    public interface ISetting
    {
        string HostName { get; }
        string ExchangeName { get; }
        string RoutingKey { get; }
        string QueueName { get; }
    }
}

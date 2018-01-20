using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Subscriber.Connection
{
    public interface IConnectionBoss
    {
        IConnection Connect();
        bool DisBand();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Socket
{
    public interface ISocketHandler
    {
        void SendData(string host, int destPort, string data);
    }
}

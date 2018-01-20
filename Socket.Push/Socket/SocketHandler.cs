using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Socket
{
    public class SocketHandler : ISocketHandler
    {
        public void SendData(string host, int destPort, string data)
        {
            IPAddress dest = Dns.GetHostAddresses(host)[0]; //Get the destination IP Address
            IPEndPoint ePoint = new IPEndPoint(dest, destPort);
            byte[] outBuffer = Encoding.ASCII.GetBytes(data); //Convert the data to a byte array
            System.Net.Sockets.Socket mySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //Create a socket using the same protocols as in the Javascript file (Dgram and Udp)
            mySocket.SendTo(outBuffer, ePoint); //Send the data to the socket
            //mySocket.Close(); //Socket use over, time to close it
        }
    }
}

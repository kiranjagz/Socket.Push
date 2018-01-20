using Autofac;
using Autofac.Core;
using Socket.Push.IoC;
using Socket.Push.Mongo;
using Socket.Push.Root;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Socket.Push
{
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Press any key to start, and 'exit' to escape");
            String data = Console.ReadLine();
            if (data.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

            var builder = new ContainerBuilder();
            builder.RegisterModule<SocketModule>();
            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var root = scope.Resolve<IRoot>();
                root.Start();
            }
            Console.Read();
        }

        //private void SimpleSend()
        //{
        //    var builder = new ContainerBuilder();
        //    builder.RegisterModule<SocketModule>();
        //    Container = builder.Build();

        //    using (var scope = Container.BeginLifetimeScope())
        //    {
        //        var setting = scope.Resolve<IMongoSetting>();
        //        var mongo = scope.Resolve<IMongoRepository>();
        //        Console.WriteLine("Press any key to start, and 'exit' to escape");
        //        String data = Console.ReadLine();
        //        if (data.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;

        //        while (true)
        //        {
        //            for (int i = 0; i < 10; i++)
        //            {
        //                var players = mongo.GetPlayers().Result.OrderBy(x => x.Rank).ToList();
        //                players.ForEach(x =>
        //                {
        //                    x.Points = x.Points + new Random().Next(1000);
        //                });

        //                var playerToJson = Newtonsoft.Json.JsonConvert.SerializeObject(players);
        //                Console.WriteLine(playerToJson);

        //                //If the user types "exit" then quit the program
        //                //for (int i = 0; i < 101; i++)
        //                //{
        //                //var message = $"This is message number: {i}";
        //                Thread.Sleep(5000);
        //                SendData("127.0.0.1", 41181, playerToJson); //Send data to that host address, on that port, with this 'data' to be sent
        //                                                            //Note the 41181 port is the same as the one we used in server.bind() in the Javascript file.

        //                System.Threading.Thread.Sleep(50); //Sleep for 50ms
        //                                                   //}
        //            }
        //        }
        //    }
        //}

        //public static void SendData(string host, int destPort, string data)
        //{
        //    IPAddress dest = Dns.GetHostAddresses(host)[0]; //Get the destination IP Address
        //    IPEndPoint ePoint = new IPEndPoint(dest, destPort);
        //    byte[] outBuffer = Encoding.ASCII.GetBytes(data); //Convert the data to a byte array
        //    System.Net.Sockets.Socket mySocket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); //Create a socket using the same protocols as in the Javascript file (Dgram and Udp)

        //    mySocket.SendTo(outBuffer, ePoint); //Send the data to the socket

        //    //mySocket.Close(); //Socket use over, time to close it
        //}
    }
}

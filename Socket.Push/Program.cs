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
            try
            {
                Console.WriteLine("Press any key to start, and 'exit' to escape");
                String data = Console.ReadLine();
                if (data.Equals("exit", StringComparison.OrdinalIgnoreCase)) return;
                Console.WriteLine("Well heres goes nothing, may the odds be forever in your favour!");
                Console.ForegroundColor = ConsoleColor.Green;
                var builder = new ContainerBuilder();
                builder.RegisterModule<SocketModule>();
                Container = builder.Build();

                using (var scope = Container.BeginLifetimeScope())
                {
                    var root = scope.Resolve<IRoot>();
                    root.Start();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Oops ze daizy, le system, shes not working, please contact someone who actually cares :): {ex.Message}");
            }
            Console.Read();
        }
    }
}

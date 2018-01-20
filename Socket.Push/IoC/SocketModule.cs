using Autofac;
using Socket.Push.Actors.Ranker;
using Socket.Push.Mongo;
using Socket.Push.Root;
using Socket.Push.Socket;
using Socket.Push.Subscriber.Connection;
using Socket.Push.Subscriber.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.IoC
{
    public class SocketModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Root.Root>().As<IRoot>();
            builder.RegisterType<ConnectionBoss>().As<IConnectionBoss>().SingleInstance();
            builder.RegisterType<Setting>().As<ISetting>();
            builder.RegisterType<MongoSetting>().As<IMongoSetting>();
            builder.RegisterType<MongoRepository>().As<IMongoRepository>().SingleInstance();
            builder.RegisterType<SocketHandler>().As<ISocketHandler>();
            builder.RegisterType<RankerHandler>().As<IRankerHandler>();

            base.Load(builder);
        }
    }
}

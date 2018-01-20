using Akka.Actor;
using Socket.Push.Actors;
using Socket.Push.Mongo;
using Socket.Push.Socket;
using Socket.Push.Subscriber;
using Socket.Push.Subscriber.Connection;
using Socket.Push.Subscriber.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Socket.Push.Root
{
    public class Root : IRoot
    {
        private ActorSystem _actorSystem;
        private IActorRef _generalActor;
        private IConnectionBoss _connectionBoss;
        private IMongoRepository _mongoRepository;
        private ISocketHandler _socketHandler;
        private ISetting _setting;


        public Root(IConnectionBoss connectionBoss, ISetting setting, IMongoRepository mongoRepository, ISocketHandler socketHandler)
        {
            _socketHandler = socketHandler;
            _connectionBoss = connectionBoss;
            _setting = setting;
            _mongoRepository = mongoRepository;
        }

        public bool Start()
        {
            _actorSystem = ActorSystem.Create("System");
            _generalActor = _actorSystem.ActorOf(Props.Create(() => new GeneralActor(_mongoRepository, _socketHandler)), "GeneralValdez");
            SubscriberClient client = new SubscriberClient(_connectionBoss.Connect(), _setting, _generalActor);
            return true;
        }

        public bool Stop()
        {
            _actorSystem.Stop(_generalActor);
            return true;
        }
    }
}

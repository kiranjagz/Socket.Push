using Akka.Actor;
using Newtonsoft.Json;
using Socket.Push.Mongo;
using Socket.Push.Mongo.Model;
using Socket.Push.Socket;
using System;
using System.Threading.Tasks;

namespace Socket.Push.Actors
{
    public class GeneralActor : ReceiveActor
    {
        private IMongoRepository _mongoRepository;
        private const string _collectionName = "generalmessages";
        private const string _trooperActorName = "trooperActor";
        private ISocketHandler _socketHandler;

        public GeneralActor(IMongoRepository monogRepository, ISocketHandler socketHandler)
        {
            _mongoRepository = monogRepository;
            _socketHandler = socketHandler;
            //Context.System.Scheduler.ScheduleTellOnce(TimeSpan.FromSeconds(20), Self, new GeneralTimerTrigger.Start(DateTime.Now), ActorRefs.Nobody);
            ReceiveAsync<GeneralMessageModel>(Handle_Message);
            Receive<GeneralTimerTrigger.Start>(_ => Handle_Trigger(_));
        }

        private void Handle_Trigger(GeneralTimerTrigger.Start _)
        {
            Console.WriteLine($"Timer schedule did run");
        }

        private async Task Handle_Message(GeneralMessageModel model)
        {
            var player = JsonConvert.DeserializeObject<PlayerDbModel>(model.EventModel.Body);
            var updatePlayer = await _mongoRepository.UpdatePlayer(player);
            Console.WriteLine($"Processing player: {JsonConvert.SerializeObject(player)}");

            var getPlayers = await _mongoRepository.GetPlayers();
            var playerToJson = Newtonsoft.Json.JsonConvert.SerializeObject(getPlayers);
            Console.WriteLine(playerToJson);
            _socketHandler.SendData("127.0.0.1", 41181, playerToJson);
        }

        //Implement somewhere
        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(// or AllForOneStrategy
                maxNrOfRetries: 10,
                withinTimeRange: TimeSpan.FromSeconds(30),
                localOnlyDecider: x =>
                {
                    if (x is ArithmeticException) return Directive.Resume;
                    else if (x is IndexOutOfRangeException) return Directive.Escalate;
                    else if (x is NotSupportedException) return Directive.Stop;
                    else return Directive.Restart;
                });
        }
    }
}

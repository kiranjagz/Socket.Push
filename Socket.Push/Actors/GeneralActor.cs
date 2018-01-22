using Akka.Actor;
using Newtonsoft.Json;
using Socket.Push.Actors.Ranker;
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
        private IRankerHandler _rankerHandler;
        private const string _collectionName = "generalmessages";
        private const string _trooperActorName = "trooperActor";
        private ISocketHandler _socketHandler;

        public GeneralActor(IMongoRepository monogRepository, ISocketHandler socketHandler, IRankerHandler rankerHandler)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            _rankerHandler = rankerHandler;
            _mongoRepository = monogRepository;
            _socketHandler = socketHandler;
            Context.System.Scheduler.ScheduleTellRepeatedly(TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(1), Self, new GeneralTimerTrigger.Start(DateTime.Now), ActorRefs.Nobody);
            ReceiveAsync<GeneralMessageModel>(Handle_Message);
            ReceiveAsync<GeneralTimerTrigger.Start>(Handle_Trigger);
        }

        private async Task Handle_Trigger(GeneralTimerTrigger.Start _)
        {
            Console.WriteLine($"Time now is: {DateTime.Now}");
            Console.WriteLine($"Timer schedule did run");
            var rankerPlayers = await _rankerHandler.RankedPlayers();
            var playerToJson = Newtonsoft.Json.JsonConvert.SerializeObject(rankerPlayers);
            Console.WriteLine(playerToJson);
            _socketHandler.SendData("127.0.0.1", 41181, playerToJson);
        }

        private async Task Handle_Message(GeneralMessageModel model)
        {
            var player = JsonConvert.DeserializeObject<PlayerDbModel>(model.EventModel.Body);
            var updatePlayer = await _mongoRepository.UpdatePlayer(player);
            Console.WriteLine($"Processing player: {JsonConvert.SerializeObject(updatePlayer)}");

            var rankerPlayers = await _rankerHandler.RankedPlayers();
            //var getPlayers = await _mongoRepository.GetPlayers();
            var playerToJson = Newtonsoft.Json.JsonConvert.SerializeObject(rankerPlayers);
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

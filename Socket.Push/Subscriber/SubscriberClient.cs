using Akka.Actor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Socket.Push.Actors;
using Socket.Push.Subscriber.Connection;
using Socket.Push.Subscriber.Settings;

namespace Socket.Push.Subscriber
{
    public class SubscriberClient
    {
        private IActorRef _generalActor;
        private ISetting _setting;
        private IConnection _connection;
        private IModel _model;
        EventingBasicConsumer _eventBasicConsumer;

        public SubscriberClient(IConnection connection, ISetting setting, IActorRef generalActor)
        {
            _generalActor = generalActor;
            _setting = setting;
            _connection = connection;
            _model = _connection.CreateModel();
            _model.BasicQos(0, 1, false);
            _eventBasicConsumer = new EventingBasicConsumer(_model);
            _eventBasicConsumer.Received += EventBasicConsumer_Received;
            _model.BasicConsume(_setting.QueueName, false, _eventBasicConsumer);
        }

        private void EventBasicConsumer_Received(object sender, BasicDeliverEventArgs e)
        {
            var eventModel = e.Map();
            _generalActor.Tell(new GeneralMessageModel(eventModel, _model));
            _model.BasicAck(e.DeliveryTag, false);
        }
    }
}

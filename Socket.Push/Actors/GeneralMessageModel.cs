using RabbitMQ.Client;
using Socket.Push.Subscriber;

namespace Socket.Push.Actors
{
    public class GeneralMessageModel
    {
        public EventModel EventModel { get; set; }
        public IModel RabbitModel { get; set; }

        public GeneralMessageModel(EventModel eventModel, IModel rabbitModel)
        {
            EventModel = eventModel;
            RabbitModel = rabbitModel;
        }
    }
}

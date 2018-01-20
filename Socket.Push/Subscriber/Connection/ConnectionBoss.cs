﻿using System;
using RabbitMQ.Client;
using System.Collections.Generic;
using Socket.Push.Subscriber.Settings;

namespace Socket.Push.Subscriber.Connection
{
    public class ConnectionBoss : IConnectionBoss
    {
        private ISetting _setting;
        private IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _model;

        public ConnectionBoss(ISetting setting)
        {
            _setting = setting;
        }

        public IConnection Connect()
        {
            _connectionFactory = new ConnectionFactory() { HostName = _setting.HostName };
            _connection = _connectionFactory.CreateConnection();
            _model = _connection.CreateModel();
            _model.ExchangeDeclare(_setting.ExchangeName, ExchangeType.Topic, true);

            var args = new Dictionary<string, object>();
            args.Add("x-message-ttl", 60000);
            _model.QueueDeclare(_setting.QueueName, true, false, false, null);
            _model.QueueBind(_setting.QueueName, _setting.ExchangeName, _setting.RoutingKey);
            return _connection;
        }

        public bool DisBand()
        {
            _connection.Close();
            return true;
        }
    }
}

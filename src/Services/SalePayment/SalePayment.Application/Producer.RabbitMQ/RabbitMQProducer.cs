using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SalePayment.Application.Producer.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public RabbitMQProducer()
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/",
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.ConfirmSelect();
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            _channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "hello", exchange: "logs", routingKey: string.Empty);
        }
        public void SendMessage<T>(T message)
        {
            
            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);
            _channel.BasicPublish(exchange: "logs",
                                 routingKey: string.Empty,
                                 basicProperties: null,
                                 body: body);
            
        }
    }
}

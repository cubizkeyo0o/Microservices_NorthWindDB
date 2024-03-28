using EventBus.Messages.Commands;
//using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections;
using System.Text;
using System.Text.Json;


namespace Customer.Application.Consumer.RabbitMQ
{
    public class RabbitMQConsumer 
    {
        private readonly IConnection _connection;
        public readonly IModel _channel;

        public RabbitMQConsumer()
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
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            _channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "hello", exchange: "logs", routingKey: string.Empty);
        }

        public async Task ReceiveMesg()
        {
            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(_channel);

            var receivedTaskCompletionSource = new TaskCompletionSource<bool>();
            string msg = "";
            consumer.Received += async (model, ea) =>
            {
                var prebody = ea.Body;
                var body = prebody.ToArray();
                string message = Encoding.UTF8.GetString(body);
                msg = message;
                // Handle message processing here
                Console.WriteLine($" [x] Received message");

                receivedTaskCompletionSource.SetResult(true);
            };

            _channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);

            await receivedTaskCompletionSource.Task;
        }  
        
    }
}

using EventBus.Messages.Commands;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

//using System.Text.Json;
using Newtonsoft.Json;
using System.Threading.Channels;
using Customer.Application.Emails;
using System.Net.Mail;
using Customer.Domain.Repositories;
using Customer.Application.Queries;
using MediatR;

namespace Customer.Application.Consumer.RabbitMQ
{
    public class RabbitMQConsumer2 : IMessageConsumer
    {
        private readonly ICustomerRepository _repository;
        private readonly IMailService _mailService;

        public RabbitMQConsumer2(ICustomerRepository repository, IMailService mailService)
        {
            _repository = repository;
            _mailService = mailService;

            //_connectionFactory = connection;
            //_connectionFactory.HostName = "localhost";
            //_connectionFactory.Port = 5672;
            //_connectionFactory.UserName = "guest";
            //_connectionFactory.Password = "guest";
            //_connectionFactory.VirtualHost = "/";

            //_connection = _connectionFactory.CreateConnection();
            //_channel = _connection.CreateModel();
            //_channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            //_channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            //_channel.QueueBind(queue: "hello", exchange: "logs", routingKey: string.Empty);
            //_channel.BasicQos(prefetchSize: 0, prefetchCount: 2, global: false);
        }
        
        private async Task<EmailMessage> MakeEmailMessage(InvoiceCheckoutCommand invoice)
        {
            while(invoice.Orders == null) { }
            //lấy thông tin customer customerName, gmail
            var customer = await _repository.GetCustomerById(invoice.Orders.CustomerId);
            //tạo ra message để gửi mail
            //tổng tiền
            decimal TotalPrice = 0;
            foreach (var item in invoice.Details)
            {
                TotalPrice += (item.Quantity * item.UnitPrice);
            }
            string mesg = $"Khách hàng đã thanh toán thành công đơn hàng {invoice.Orders.OrderId} với tổng số tiền là {TotalPrice} gồm những sản phẩm sau: \n";
            //list sản phẩm
            string product = "";
            decimal priceProduct = 0;
            foreach (var item in invoice.ProductInfos)
            {
                foreach (var item2 in invoice.Details)
                {
                    if (item.ProductId == item2.ProductId)
                    {
                        priceProduct = item2.UnitPrice;
                        break;
                    }
                }
                product += $"Mã sản phẩm: {item.ProductId}; tên sp: {item.ProductName}; Category: {item.CategoryName}; QuantityPerUnit: {item.QuantityPerUnit}; giá: {priceProduct} \n";
            }
            mesg += product;
            EmailMessage emailMessage = new EmailMessage()
            {
                ReceiverEmail = customer.Gmail,
                ReceiverName = customer.CompanyName,
                Body = mesg,
                Title = "Confirm successful payment of order"
            };
            Console.WriteLine($"{invoice.Orders.OrderId}  {customer.CustomerId}");
            return emailMessage;
        }

        public async Task<bool> Test()
        {
            Console.WriteLine(" [*] Waiting for messages.");

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };
            using var _connection = factory.CreateConnection();
            using var _channel = _connection.CreateModel();
            _channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            _channel.QueueDeclare(queue: "hello", durable: true, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: "hello", exchange: "logs", routingKey: string.Empty);
            _channel.BasicQos(0, 1, false);

            var consumer = new EventingBasicConsumer(_channel);
            string msg = "";
            InvoiceCheckoutCommand invoice = new InvoiceCheckoutCommand();
            _channel.BasicConsume(queue: "hello", autoAck: false, consumer: consumer);
            var signal2 = new AutoResetEvent(false);
            var signal = new AutoResetEvent(false);
            consumer.Received += (model, ea) =>
            {
                //lấy thông tin order, orderdetail, products
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                msg = message;
                // Handle message processing here
                invoice = JsonConvert.DeserializeObject<InvoiceCheckoutCommand>(msg);
                signal.Set();
                signal2.WaitOne();
                _channel.BasicAck(ea.DeliveryTag, false);

                Console.WriteLine($" [x] Received message");
            };
            while (true)
            {
                signal.WaitOne();
                
                var mesgmail = await MakeEmailMessage(invoice);
                var checkSendMail = _mailService.SendEmailAsync(mesgmail);
                if (checkSendMail) Console.WriteLine($"đã gửi mail thành công");
                signal2.Set();
            }
        }
    }
}
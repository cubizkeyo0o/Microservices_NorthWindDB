using EventBus.Messages.Commands;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Customer.Application.Consumer.RabbitMQ
{
    public interface IMessageConsumer
    {
        Task<bool> Test();
    }
}

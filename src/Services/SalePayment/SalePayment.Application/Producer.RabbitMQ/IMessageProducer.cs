using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Application.Producer.RabbitMQ
{
    public interface IMessageProducer
    {
        void SendMessage<T>(T message);
    }
}

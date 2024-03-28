using MediatR;
using SalePayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Application.Queries
{
    public class GetOrderDetailByOrderIdQuery : IRequest<List<OrderDetail>>
    {
        public int OrderId { get; set; }
        public GetOrderDetailByOrderIdQuery(int orderId)
        {
            OrderId = orderId;
        }   
    }
}

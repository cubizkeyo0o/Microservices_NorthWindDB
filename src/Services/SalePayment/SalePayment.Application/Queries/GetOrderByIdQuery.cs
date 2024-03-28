using MediatR;
using SalePayment.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Application.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int Id { get; set; }
        public GetOrderByIdQuery(int id)
        {
            this.Id = id;
        }
    }
}

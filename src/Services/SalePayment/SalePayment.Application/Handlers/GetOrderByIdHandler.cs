using MediatR;
using SalePayment.Application.Queries;
using SalePayment.Domain.Entities;
using SalePayment.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalePayment.Application.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderRepository _repository;
        public GetOrderByIdHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order =await _repository.GetOrderById(request.Id);
            if(order == null)
            {
                throw new NotImplementedException();
            }
            return order;
        }
    }
}

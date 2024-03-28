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
    public class GetOrderDetailByOrderIdHandler : IRequestHandler<GetOrderDetailByOrderIdQuery, List<OrderDetail>>
    {
        private readonly IOrderRepository _repository;
        public GetOrderDetailByOrderIdHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Task<List<OrderDetail>> Handle(GetOrderDetailByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var listOrderDetail = _repository.GetOrderDetailByOrderId(request.OrderId);
            if(listOrderDetail == null)
            {
                throw new NotImplementedException();
            }
            return listOrderDetail;
        }
    }
}

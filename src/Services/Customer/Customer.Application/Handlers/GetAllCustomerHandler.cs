using Customer.Application.Queries;
using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Handlers
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerModel>>
    {
        private readonly ICustomerRepository _repository;
        public GetAllCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<CustomerModel>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var ListCustomers = await _repository.GetAllCustomers();
            if(ListCustomers == null)
            {
                throw new NotImplementedException();
            }
            return ListCustomers;
        }
    }
}

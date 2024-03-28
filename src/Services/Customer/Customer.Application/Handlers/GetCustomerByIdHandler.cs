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
    public class GetCustomerByIdHandler : IRequestHandler<GetCustomerByIdQuery, CustomerModel>
    {
        private readonly ICustomerRepository _repository;
        public GetCustomerByIdHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }
        public async Task<CustomerModel> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetCustomerById(request.id);
        }
    }
}

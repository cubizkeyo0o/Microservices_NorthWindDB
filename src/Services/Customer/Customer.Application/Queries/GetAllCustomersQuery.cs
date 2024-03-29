using Customer.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Application.Queries
{
    public class GetAllCustomersQuery : IRequest<List<CustomerModel>>
    {
    }
}

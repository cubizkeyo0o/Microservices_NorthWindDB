using Catalog.Application.Responses;
using Catalog.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductGrpcQuery : IRequest<ProductReply>
    {
        public List<int> listProductId { get; set; }
        public GetProductGrpcQuery(List<int> listproductid)
        {
            listProductId = listproductid;
        }
    }
}

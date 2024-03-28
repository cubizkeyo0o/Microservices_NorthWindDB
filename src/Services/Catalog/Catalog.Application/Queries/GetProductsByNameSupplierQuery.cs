
using Catalog.Domain.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Queries
{
    public class GetProductsByNameSupplierQuery : IRequest<List<ProductDTO>>
    {
        public string name { get; set; }
        public GetProductsByNameSupplierQuery(string name)
        {
            this.name = name;
        }
    }
}

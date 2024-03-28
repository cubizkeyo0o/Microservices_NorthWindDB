using Catalog.Application.Queries;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Handlers
{
    public class GetAllSupplierHandler : IRequestHandler<GetAllSupplierQuery, List<Supplier>>
    {
        private readonly ISupplierRepository _repository;
        public GetAllSupplierHandler(ISupplierRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<Supplier>> Handle(GetAllSupplierQuery request, CancellationToken cancellationToken)
        {
            var ListSupplier =await _repository.GetAllSupplier();
            if(ListSupplier == null)
            {
                throw new NotImplementedException();
            }
            return ListSupplier;
        }
    }
}

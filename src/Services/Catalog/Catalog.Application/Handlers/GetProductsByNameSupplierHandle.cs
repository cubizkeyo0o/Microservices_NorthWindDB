using Catalog.Application.Queries;
using Catalog.Domain.Data;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductsByNameSupplierHandle : IRequestHandler<GetProductsByNameSupplierQuery, List<ProductDTO>>
    {
        private readonly IProductRepository _repository;
        public GetProductsByNameSupplierHandle(IProductRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ProductDTO>> Handle(GetProductsByNameSupplierQuery request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetProductByNameSupplier(request.name);

            return products;
        }
    }
}

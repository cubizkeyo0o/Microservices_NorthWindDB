using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetAllProductsHandle : IRequestHandler<GetAllProductsQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetAllProductsHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productList =await _repository.GetAllProducts();
            
            return _mapper.Map<List<ProductResponse>>(productList);
        }
    }
}

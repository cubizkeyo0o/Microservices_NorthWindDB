using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByNameHandle : IRequestHandler<GetProductByNameQuery, List<ProductResponse>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductByNameHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductResponse>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
        {
            var productList =await _repository.GetProductByName(request.Name);
            
            return _mapper.Map<List<ProductResponse>>(productList);
        }
    }
}

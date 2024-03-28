using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Repositories;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductByIdHandle : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductByIdHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product =await _repository.GetProductById(request.Id);
            
            return _mapper.Map<ProductResponse>(product);
        }
    }
}

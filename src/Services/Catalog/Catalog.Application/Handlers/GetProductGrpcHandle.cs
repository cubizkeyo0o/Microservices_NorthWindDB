using AutoMapper;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Entities;
using Catalog.Domain.Repositories;
using Google.Protobuf;
using MediatR;

namespace Catalog.Application.Handlers
{
    public class GetProductGrpcHandle : IRequestHandler<GetProductGrpcQuery, ProductReply>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetProductGrpcHandle(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductReply> Handle(GetProductGrpcQuery request, CancellationToken cancellationToken)
        {
            List<ProductGrpcService> products =await _repository.GetProductGrpc(request.listProductId);
            //var result = _mapper.Map<List<ProductGrpc>>(products);

            List<ProductGrpc> productGrpcs = new List<ProductGrpc>();
            if(products != null)
            {
                foreach(var item in products)
                {
                productGrpcs.Add(new ProductGrpc()
                    {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    CompanyName = item.CompanyName,
                    CategoryName = item.CategoryName,
                    QuantityPerUnit = item.QuantityPerUnit,
                    UnitsInStock = item.UnitsInStock,
                    });;
                }
            }
            

            ProductReply produtReply = new ProductReply();
            produtReply.Productgrpc.AddRange(productGrpcs);
            return produtReply;
        }
    }
}

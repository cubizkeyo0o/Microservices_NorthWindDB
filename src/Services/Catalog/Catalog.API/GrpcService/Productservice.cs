using Grpc.Core;
using System.Threading.Tasks;

using MediatR;
using Catalog.Application.Queries;
using Catalog.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Catalog.Infrastructure.Data;
namespace Catalog.API.GrpcService
{
    public class ProductService : GetProductInfo.GetProductInfoBase
    {
        private readonly IMediator _mediator;
        //private readonly CatalogContext _context;
        public ProductService(IMediator mediator/*, CatalogContext context*/)
        {
            _mediator = mediator;
            //_context = context;
        }
        public override async Task<ProductReply> GetProductById(ProductIdRequest request, ServerCallContext context)
        {
            List<int> listIds = new List<int>();

            foreach(var item in request.Id)
            {
                listIds.Add(item);
            }

            var query = new GetProductGrpcQuery(listIds);
            return await _mediator.Send(query);
            
        }
    }
    
}

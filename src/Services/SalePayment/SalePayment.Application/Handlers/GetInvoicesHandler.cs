using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalePayment.Application.Queries;
using SalePayment.Domain.Entities;
using SalePayment.Domain.Repositories;
using SalePayment.Application.Response;

namespace SalePayment.Application.Handlers
{
    public class GetInvoicesHandler : IRequestHandler<GetInvoicesQuery, InvoiceResponse>
    {
        private readonly IOrderRepository _repository;
        private readonly GetProductInfo.GetProductInfoClient _client;
        public GetInvoicesHandler(IOrderRepository repository, GetProductInfo.GetProductInfoClient client)
        {
            _repository = repository;
            _client = client;
        }
        public async Task<InvoiceResponse> Handle(GetInvoicesQuery request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetOrderById(request.Id);
            var orderDetail = await _repository.GetOrderDetailByOrderId(request.Id);

            //get productId from orderDetail
            List<int> productIds = new List<int>();
            foreach (var item in orderDetail)
            {
                productIds.Add(item.ProductId);
            }

            //sử dụng grpc client để lấy productInfo
            ProductIdRequest requestGrpc = new ProductIdRequest();
            requestGrpc.Id.AddRange(productIds);    //add list id product vào request
            var ListProductGrpc =await _client.GetProductByIdAsync(requestGrpc);
            List<ProductInfo> productInfos = new List<ProductInfo>();
            foreach (var item in ListProductGrpc.Productgrpc)
            {
                productInfos.Add(new ProductInfo()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    CompanyName = item.CompanyName,
                    CategoryName = item.CategoryName,
                    QuantityPerUnit = item.QuantityPerUnit,
                    UnitsInStock = item.UnitsInStock,
                });
            }

            return new InvoiceResponse()
            {
                Orders = order,
                Details = orderDetail,
                ProductInfos = productInfos,
            };
        }

        
    }
}

using Azure.Core;
using Catalog.API.GrpcService;
using Catalog.Application.Queries;
using Catalog.Application.Responses;
using Catalog.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Controllers;
//using Newtonsoft.Json;
using System.Text.Json;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        //setup _option để convert List<T> thành json dễ đọc hơn
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        private readonly IMediator _mediator;
        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        [Route("getproductgrpc")]
        public async Task<ActionResult<List<ProductGrpc>>> GetProductGrpc()
        {
            List<int> listIds = new List<int> { 11,42,72};
            var query = new GetProductGrpcQuery(listIds);
            var products =await _mediator.Send(query);
            return Ok(products.Productgrpc);
        }

        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<ActionResult<List<ProductResponse>>> GetAllProduct()
        {

            var query = new GetAllProductsQuery();
            var productList = await _mediator.Send(query);
            //truyền _options để chuyển productList thành json dễ đọc hơn
            var jsonconvert = JsonSerializer.Serialize(productList, _options);
            return Ok(jsonconvert);
        }

        [HttpGet]
        [Route("GetProductByName/{name}")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductByName(string name)
        {

            var query = new GetProductByNameQuery(name);
            var productList= await _mediator.Send(query);
            //truyền _options để chuyển productList thành json dễ đọc hơn
            var jsonconvert = JsonSerializer.Serialize(productList, _options);
            return Ok(jsonconvert);
        }

        [HttpGet]
        [Route("GetProductById/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProductById(int id)
        {

            var query = new GetProductByIdQuery(id);
            var product = await _mediator.Send(query);
            //truyền _options để chuyển productList thành json dễ đọc hơn
            var jsonconvert = JsonSerializer.Serialize(product, _options);
            return Ok(jsonconvert);
        }

        [HttpGet]
        [Route("GetProductByNameSupplier/{name}")]
        public async Task<ActionResult<List<ProductResponse>>> GetProductByNameSupplier(string name)
        {

            var query = new GetProductsByNameSupplierQuery(name);
            var product = await _mediator.Send(query);
            //truyền _options để chuyển productList thành json dễ đọc hơn
            var jsonconvert = JsonSerializer.Serialize(product, _options);
            return Ok(jsonconvert);
        }

        [HttpGet]
        [Route("GetAllSupplier")]
        public async Task<ActionResult<List<Supplier>>> GetAllSupplier()
        {
            var query = new GetAllSupplierQuery();
            var supplierList =await _mediator.Send(query);
            //truyền _options để chuyển productList thành json dễ đọc hơn
            var jsonconvert = JsonSerializer.Serialize(supplierList, _options);
            return supplierList;
        }
    }
}

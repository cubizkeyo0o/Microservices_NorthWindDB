using AutoMapper;
using Catalog.Application.Responses;
using Catalog.Domain.Data;
using Catalog.Domain.Entities;
using Google.Protobuf;


namespace Catalog.Application.Mapper
{
    public class MappingProfile : Profile
    {
 
        public MappingProfile()
        {
            CreateMap<Product, ProductResponse>();
            CreateMap<ProductDTO, ProductResponse>();
            CreateMap<ProductGrpcService, ProductGrpc>();
        }
    }
}

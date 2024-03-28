using AutoMapper;
using EventBus.Messages.Commands;
using SalePayment.Application.Response;
using SalePayment.Domain.Entities;


namespace SalePayment.Application.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderCmd>();
            CreateMap<OrderDetail, OrderDetailCmd>();
            CreateMap<ProductInfo, ProductInfoCmd>();
            CreateMap<InvoiceResponse, InvoiceCheckoutCommand>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Orders))
                .ForMember(dest => dest.Details, opt => opt.MapFrom(src => src.Details))
                .ForMember(dest => dest.ProductInfos, opt => opt.MapFrom(src => src.ProductInfos));


            CreateMap<SalePayment.Domain.Entities.Order, EventBus.Messages.Commands.OrderCmd>();
            CreateMap<SalePayment.Domain.Entities.OrderDetail, EventBus.Messages.Commands.OrderDetailCmd>();
            CreateMap<SalePayment.Domain.Entities.ProductInfo, EventBus.Messages.Commands.ProductInfoCmd>();
        }
    }
}

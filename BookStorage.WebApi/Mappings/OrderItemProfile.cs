using AutoMapper;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.OrderItem;

namespace BookStorage.WebApi.Mapping
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemViewDto>();
            CreateMap<UpdateOrderItemDto, OrderItem>();
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}

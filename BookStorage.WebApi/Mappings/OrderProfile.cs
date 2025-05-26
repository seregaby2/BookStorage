using AutoMapper;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Order;

namespace BookStorage.WebApi.Mapping
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderViewDto>();
            CreateMap<UpdateOrderDto, Order>();
            CreateMap<CreateOrderDto, Order>();
        }
    }
}
using AutoMapper;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Book;
using BookStorage.WebApi.DTOs.Customer;
using BookStorage.WebApi.DTOs.Order;
using BookStorage.WebApi.DTOs.OrderBook;

namespace BookStorage.WebApi.Mapping
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, OrderViewDto>()
				.ForMember(dest => dest.Books, opt => opt.MapFrom(src => src.OrderBooks))
				.ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));

			CreateMap<OrderBook, OrderBookViewDto>()
				.ForMember(dest => dest.BookId, opt => opt.MapFrom(src => src.BookId))
				.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
				.ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Book.Price));

			CreateMap<Book, BookViewDto>();

			CreateMap<Customer, CustomerViewDto>();

			CreateMap<UpdateOrderBookDto, OrderBook>();

			CreateMap<UpdateOrderDto, Order>()
				.ForMember(dest => dest.OrderBooks, opt => opt.MapFrom(src => src.Books));

			CreateMap<CreateOrderBookDto, OrderBook>();

			CreateMap<CreateOrderDto, Order>()
			.ForMember(dest => dest.OrderBooks, opt => opt.MapFrom(src => src.Books));
		}
	}
}



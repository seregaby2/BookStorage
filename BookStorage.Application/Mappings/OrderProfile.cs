using AutoMapper;
using BookStorage.Application.Commands.Order.Create;
using BookStorage.Application.Commands.Order.Update;
using BookStorage.Application.Queries.Book.GetAll;
using BookStorage.Application.Queries.Customer.GetAll;
using BookStorage.Application.Queries.Order.GetAll;
using BookStorage.Application.Queries.Order.GetById;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Mappings
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			CreateMap<Order, GetAllOrderModel>();
			CreateMap<Order, GetByIdOrderModel>();

			CreateMap<Customer, GetAllCustomerModel>();
			CreateMap<Book, GetAllBooksModel>();

			CreateMap<CreateOrderModel, Order>()
			  .ForMember(dest => dest.OrderBooks, opt => opt.MapFrom(src =>
				  src.OrderBook.Select(b => new OrderBook
				  {
					  BookId = b.BookId,
					  Quantity = b.Quantity
				  })))
			  .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(_ => DateTime.UtcNow))
			  .ForMember(dest => dest.TotalAmount, opt => opt.Ignore());

			CreateMap<Order, CreateOrderModel>()
				.ForMember(dest => dest.OrderBook, opt => opt.MapFrom(src =>
					src.OrderBooks.Select(ob => new BookOrderItemModel
					{
						BookId = ob.BookId,
						Quantity = ob.Quantity
					})));

			CreateMap<Order, UpdateOrderModel>()
				.ForMember(dest => dest.OrderBook, opt => opt.MapFrom(src =>
					src.OrderBooks.Select(ob => new BookOrderItemModel
					{
						BookId = ob.BookId,
						Quantity = ob.Quantity
					})));

			CreateMap<UpdateOrderModel, Order>()
				.ForMember(dest => dest.OrderBooks, opt => opt.MapFrom(src =>
					src.OrderBook.Select(b => new OrderBook
					{
						BookId = b.BookId,
						Quantity = b.Quantity
					})))
				.ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
				.ForMember(dest => dest.OrderDate, opt => opt.Ignore());
		}
	}
}

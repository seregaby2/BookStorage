using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class OrderService : IOrderService
	{
		private readonly IBookRepository _bookRepository;

		public OrderService(IBookRepository bookRepository)
		{
			_bookRepository = bookRepository;
		}

		public async Task CountTotalAmount(Order order)
		{
			foreach (var ob in order.OrderBooks)
			{
				var book = await _bookRepository.GetByIdAsync(ob.BookId);
				ob.Book = book;
			}

			order.TotalAmount = order.OrderBooks.Sum(ob => ob.Quantity * (ob.Book?.Price ?? 0));
		}
	}
}

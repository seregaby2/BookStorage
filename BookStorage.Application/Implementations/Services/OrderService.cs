using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IBookRepository _bookRepository;
		private readonly ICustomerRepository _customerRepository;

		public OrderService(IOrderRepository orderRepository, IBookRepository bookRepository, ICustomerRepository customerRepository)
		{
			_orderRepository = orderRepository;
			_bookRepository = bookRepository;
			_customerRepository = customerRepository;
		}

		public async Task<IEnumerable<Order>> GetAll()
		{
			return await _orderRepository.GetAllAsync();
		}

		public async Task<Order?> GetById(Guid id)
		{
			return await _orderRepository.GetByIdAsync(id);
		}

		public async Task<Order?> Create(Order order)
		{
			var customerExists = await _customerRepository.GetByIdAsync(order.CustomerId);
			if (customerExists == null)
				return null;

			foreach (var ob in order.OrderBooks)
			{
				var bookExists = await _bookRepository.GetByIdAsync(ob.BookId);
				if (bookExists == null)
					return null;
			}

			await CountTotalAmount(order);

			return await _orderRepository.CreateAsync(order);
		}

		public async Task<Order?> Update(Guid id, Order order)
		{
			var existingOrder = await _orderRepository.GetByIdAsync(id);
			if (existingOrder == null)
				return null;

			var customerExists = await _customerRepository.GetByIdAsync(order.CustomerId);
			if (customerExists == null)
				return null;

			foreach (var ob in order.OrderBooks)
			{
				var bookExists = await _bookRepository.GetByIdAsync(ob.BookId);
				if (bookExists == null)
					return null;
			}

			await CountTotalAmount(order);

			return await _orderRepository.UpdateAsync(id, order);
		}

		public async Task<bool> Delete(Guid id)
		{
			var orderToDelete = await _orderRepository.GetByIdAsync(id);
			if (orderToDelete == null)
				return false;


			return await _orderRepository.DeleteAsync(id);
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

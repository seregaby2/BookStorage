using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _repository;

		public OrderService(IOrderRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Order>> GetAll()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Order?> GetById(Guid id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Order?> Create(Order order, List<Guid> bookIds)
		{
			if (bookIds == null || bookIds.Count == 0)
			{
				throw new ArgumentException("Order must contain at least one book.", nameof(bookIds));
			}

			return await _repository.CreateAsync(order, bookIds);
		}

		public async Task<bool?> Update(Guid id, Order order, List<Guid> bookIds)
		{
			var existingOrder = await _repository.GetByIdAsync(id);
			if (existingOrder == null)
				return null;

			return await _repository.UpdateAsync(id, order, bookIds);
		}

		public async Task<bool> Delete(Guid id)
		{
			var orderToDelete = await _repository.GetByIdAsync(id);
			if (orderToDelete == null)
				return false;

			return await _repository.DeleteAsync(id);
		}
	}
}

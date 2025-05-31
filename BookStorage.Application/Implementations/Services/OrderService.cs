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

		public async Task<Order?> Create(Order order)
		{
			return await _repository.CreateAsync(order);
		}

		public async Task<Order?> Update(Guid id, Order order)
		{
			var existingOrder = await _repository.GetByIdAsync(id);
			if (existingOrder == null)
				return null;

			return await _repository.UpdateAsync(id, order);
		}

		public async Task<bool> Delete(Guid id)
		{
			var orderToDelete = await _repository.GetByIdAsync(id);
			if (orderToDelete == null)
				return false;

			return await _repository.DeleteAsync(id);
		}

		Task<bool?> IOrderService.Update(Guid id, Order order)
		{
			throw new NotImplementedException();
		}
	}
}

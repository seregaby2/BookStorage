using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IOrderService
	{
		Task<IEnumerable<Order>> GetAll();
		Task<Order?> GetById(Guid id);
		Task<Order?> Create(Order order);
		Task<Order?> Update(Guid id, Order order);
		Task<bool> Delete(Guid id);

		Task CountTotalAmount(Order order);
	}
}

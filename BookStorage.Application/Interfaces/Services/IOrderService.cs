using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IOrderService
	{
		Task<IEnumerable<Order>> GetAll();
		Task<Order?> GetById(Guid id);
		Task<Order?> Create(Order order, List<Guid> bookIds);
		Task<bool?> Update(Guid id, Order order, List<Guid>? bookIds);
		Task<bool> Delete(Guid id);
	}
}

using BookStorage.Domain.Models;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IOrderRepository
	{
		Task<IEnumerable<Order>> GetAllAsync();
		Task<Order?> GetByIdAsync(Guid id);
		Task<Order?> CreateAsync(Order order, List<Guid> bookIds);
		Task<bool?> UpdateAsync(Guid id, Order order, List<Guid>? bookIds);
		Task<bool> DeleteAsync(Guid id);
	}
}

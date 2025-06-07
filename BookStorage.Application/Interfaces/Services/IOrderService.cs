using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IOrderService
	{
		Task CountTotalAmount(Order order);
	}
}

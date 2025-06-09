using BookStorage.Domain.Models;
using System.Data;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IOrderBookRepository
	{
		Task InsertUpdateBook(Order order, IDbConnection connection, IDbTransaction transaction);
		Task DeleteOrderBookItem(Guid id, IDbConnection connection, IDbTransaction transaction);
	}
}

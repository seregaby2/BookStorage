
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;
using System.Data;

namespace BookStorage.Infrastructure.Data.Repositories
{
	class OrderBookRepository : IOrderBookRepository
	{

		public async Task InsertUpdateBook(Order order, IDbConnection connection, IDbTransaction transaction)
		{
			const string updateBooks = @"
					INSERT INTO store.OrderBooks (OrderId, BookId, Quantity)
					VALUES (@OrderId, @BookId, @Quantity);";

			foreach (var ob in order.OrderBooks)
			{
				await connection.ExecuteScalarAsync(updateBooks, new
				{
					OrderId = order.Id,
					ob.BookId,
					ob.Quantity
				}, transaction);
			}
		}

		public async Task DeleteOrderBookItem(Guid id, IDbConnection connection, IDbTransaction transaction)
		{
			var query = "DELETE FROM store.OrderBooks WHERE OrderId = @OrderId";

			await connection.ExecuteAsync(query, new { OrderId = id }, transaction);
		}
	}
}

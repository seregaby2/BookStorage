using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IDbConnectionFactory _dbFactory;

		public OrderRepository(IDbConnectionFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public async Task<IEnumerable<Order>> GetAllAsync()
		{
			const string query = @"
				SELECT
					o.Id, o.OrderDate, o.Status, o.TotalAmount, o.CustomerId,
					c.Id, c.Email, c.FirstName, c.PhoneNumber, c.PurchaseDate,
					b.Id, b.Title, b.Genre, b.Price, b.PublishDate, b.AuthorId, b.OrderId
				FROM store.Orders o
				JOIN store.Customers c ON o.CustomerId = c.Id
				LEFT JOIN store.Books b ON o.Id = b.OrderId";

			using var connection = _dbFactory.CreateConnection();

			var orderDictionary = new Dictionary<Guid, Order>();

			var result = await connection.QueryAsync<Order, Customer, Book, Order>(
				query,
				(order, customer, book) =>
				{
					if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
					{
						currentOrder = order;
						currentOrder.Customer = customer;
						currentOrder.Books = new List<Book>();
						orderDictionary.Add(order.Id, currentOrder);
					}

					if (book != null && book.Id != Guid.Empty)
					{
						currentOrder.Books.Add(book);
					}

					return currentOrder;
				},
				splitOn: "Id,Id"
			);

			return orderDictionary.Values;
		}

		public async Task<Order?> GetByIdAsync(Guid id)
		{
			const string query = @"
                SELECT
					o.*,
					c.Id, c.Email, c.FirstName, c.PhoneNumber, c.PurchaseDate,
                    b.Id, b.Title, b.Price, b.AuthorId, b.OrderId
                FROM store.Orders o
                JOIN store.Customers c ON o.CustomerId = c.Id
                LEFT JOIN store.Books b ON o.Id = b.OrderId
                WHERE o.Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var orderDictionary = new Dictionary<Guid, Order>();

			var result = await connection.QueryAsync<Order, Customer, Book, Order>(
				query,
				(order, customer, book) =>
				{
					if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
					{
						currentOrder = order;
						currentOrder.Customer = customer;
						currentOrder.Books = new List<Book>();
						orderDictionary.Add(order.Id, currentOrder);
					}

					if (book != null && book.Id != Guid.Empty)
					{
						currentOrder.Books.Add(book);
					}

					return currentOrder;
				},
				new { Id = id },
				splitOn: "Id,Id"
			);

			return orderDictionary.Values.FirstOrDefault();
		}

		public async Task<Order?> CreateAsync(Order order, List<Guid> bookIds)
		{
			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();
			using var transaction = connection.BeginTransaction();

			try
			{
				order.Id = Guid.NewGuid();
				order.OrderDate = DateTime.UtcNow;

				const string insertOrder = @"
                    INSERT INTO store.Orders (Id, CustomerId, OrderDate, Status, TotalAmount)
                    VALUES (@Id, @CustomerId, @OrderDate, @Status, @TotalAmount)";

				await connection.ExecuteAsync(insertOrder, order, transaction);

				const string updateBooks = @"
                    UPDATE store.Books SET OrderId = @OrderId WHERE Id = @BookId";

				foreach (var bookId in bookIds)
				{
					await connection.ExecuteAsync(updateBooks, new { OrderId = order.Id, BookId = bookId }, transaction);
				}

				transaction.Commit();
				return order;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public async Task<Order?> UpdateAsync(Guid id, Order order, List<Guid>? bookIds = null)
		{
			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();
			using var transaction = connection.BeginTransaction();

			try
			{
				const string updateOrder = @"
                    UPDATE store.Orders
                    SET CustomerId = @CustomerId, Status = @Status, TotalAmount = @TotalAmount
                    WHERE Id = @Id";

				var rowsAffected = await connection.ExecuteAsync(updateOrder, new
				{
					Id = id,
					order.CustomerId,
					order.Status,
					order.TotalAmount
				}, transaction);

				if (bookIds != null)
				{
					const string clearBooks = @"
                        UPDATE store.Books SET OrderId = NULL WHERE OrderId = @OrderId";
					await connection.ExecuteAsync(clearBooks, new { OrderId = id }, transaction);

					const string updateBooks = @"
                        UPDATE store.Books SET OrderId = @OrderId WHERE Id = @BookId";
					foreach (var bookId in bookIds)
					{
						await connection.ExecuteAsync(updateBooks, new { OrderId = id, BookId = bookId }, transaction);
					}
				}

				transaction.Commit();
				return rowsAffected > 0 ? order : null;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();
			using var transaction = connection.BeginTransaction();

			try
			{
				const string detachBooks = @"
                    UPDATE store.Books SET OrderId = NULL WHERE OrderId = @OrderId";

				await connection.ExecuteAsync(detachBooks, new { OrderId = id }, transaction);

				const string deleteOrder = "DELETE FROM store.Orders WHERE Id = @Id";

				var rowsAffected = await connection.ExecuteAsync(deleteOrder, new { Id = id }, transaction);

				transaction.Commit();
				return rowsAffected > 0;
			}
			catch
			{
				transaction.Rollback();
				throw;
			}
		}
	}
}

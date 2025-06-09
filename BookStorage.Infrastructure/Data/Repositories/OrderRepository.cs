using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IDbConnectionFactory _dbFactory;
		private readonly IOrderBookRepository _orderBookRepository;

		public OrderRepository(IDbConnectionFactory dbFactory, IOrderBookRepository orderBookRepository)
		{
			_dbFactory = dbFactory;
			_orderBookRepository = orderBookRepository;
		}

		public async Task<IEnumerable<Order>> GetAllAsync()
		{
			const string query = @"
				SELECT 
					o.Id, o.CustomerId, o.OrderDate, o.Status, o.TotalAmount,
					c.Id, c.Email, c.FirstName, c.PhoneNumber, c.PurchaseDate,
					ob.BookId, ob.Quantity,
					b.Id, b.Title, b.Price
				FROM store.Orders o
				INNER JOIN store.Customers c ON o.CustomerId = c.Id
				LEFT JOIN store.OrderBooks ob ON o.Id = ob.OrderId
				LEFT JOIN store.Books b ON ob.BookId = b.Id
				ORDER BY o.OrderDate DESC";

			using var connection = _dbFactory.CreateConnection();

			var orderDictionary = new Dictionary<Guid, Order>();

			var orders = await connection.QueryAsync<Order, Customer, OrderBook, Book, Order>(
				query,
				(order, customer, orderBook, book) =>
				{
					if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
					{
						currentOrder = order;
						currentOrder.Customer = customer;
						currentOrder.OrderBooks = new List<OrderBook>();
						orderDictionary.Add(order.Id, currentOrder);
					}

					if (orderBook != null && book != null)
					{
						orderBook.Book = book;
						orderBook.Order = currentOrder;
						currentOrder.OrderBooks.Add(orderBook);
					}

					return currentOrder;
				},
				splitOn: "Id,BookId,Id");

			return orderDictionary.Values;
		}

		public async Task<Order?> GetByIdAsync(Guid id)
		{
			const string query = @"
				SELECT o.*, c.*, ob.BookId, ob.Quantity, b.*
				FROM store.Orders o
				INNER JOIN store.Customers c ON o.CustomerId = c.Id
				LEFT JOIN store.OrderBooks ob ON o.Id = ob.OrderId
				LEFT JOIN store.Books b ON ob.BookId = b.Id
				WHERE o.Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var orderDictionary = new Dictionary<Guid, Order>();

			var orders = await connection.QueryAsync<Order, Customer, Guid?, int?, Book, Order>(
		query,
		(order, customer, bookId, quantity, book) =>
		{
			if (!orderDictionary.TryGetValue(order.Id, out var currentOrder))
			{
				currentOrder = order;
				currentOrder.Customer = customer;
				currentOrder.OrderBooks = new List<OrderBook>();
				orderDictionary.Add(order.Id, currentOrder);
			}

			if (bookId.HasValue && book != null)
			{
				currentOrder.OrderBooks.Add(new OrderBook
				{
					OrderId = order.Id,
					BookId = bookId.Value,
					Quantity = quantity ?? 1,
					Book = book,
					Order = currentOrder
				});
			}

			return currentOrder;
		},
		new { Id = id },
		splitOn: "Id,BookId,Quantity,Id"
	);

			return orderDictionary.Values.FirstOrDefault();
		}

		public async Task<Order?> CreateAsync(Order order)
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

				await connection.ExecuteAsync(insertOrder, new
				{
					order.Id,
					order.OrderDate,
					Status = (int)order.Status,
					order.TotalAmount,
					order.CustomerId
				}, transaction);

				await _orderBookRepository.InsertUpdateBook(order, connection, transaction);
				transaction.Commit();

				return order;
			}
			catch (Exception ex)
			{
				transaction.Rollback();
				Console.WriteLine("Error when creating an order: " + ex.Message);
				throw;
			}
		}

		public async Task<Order?> UpdateAsync(Guid id, Order order)
		{
			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();
			using var transaction = connection.BeginTransaction();

			try
			{
				var existingOrder = await connection.QuerySingleOrDefaultAsync<Order>(
					"SELECT * FROM store.Orders WHERE Id = @Id",
					new { Id = id }, transaction);

				if (existingOrder == null)
					return null;

				var updateOrderSql = @"
					UPDATE store.Orders SET 
					    Status = @Status,
					    TotalAmount = @TotalAmount,
					    CustomerId = @CustomerId
					WHERE Id = @Id";

				await connection.ExecuteAsync(updateOrderSql, new
				{
					Id = id,
					order.OrderDate,
					Status = (int)order.Status,
					order.TotalAmount,
					order.CustomerId
				}, transaction);

				order.Id = id;
				foreach (var ob in order.OrderBooks)
				{
					ob.OrderId = id;
				}

				await _orderBookRepository.DeleteOrderBookItem(id, connection, transaction);

				await _orderBookRepository.InsertUpdateBook(order, connection, transaction);
				await transaction.CommitAsync();

				order.Id = id;
				return order;
			}
			catch
			{
				await transaction.RollbackAsync();
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
				var existingOrder = await connection.QuerySingleOrDefaultAsync<Order>(
					"SELECT * FROM store.Orders WHERE Id = @Id", new { Id = id }, transaction);

				if (existingOrder == null)
					return false;

				await _orderBookRepository.DeleteOrderBookItem(id, connection, transaction);

				var affectedRows = await connection.ExecuteAsync("DELETE FROM store.Orders WHERE Id = @Id", new { Id = id }, transaction);

				await transaction.CommitAsync();

				return affectedRows > 0;
			}
			catch
			{
				await transaction.RollbackAsync();
				throw;
			}
		}
	}
}

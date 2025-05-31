using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class CustomerRepository : ICustomerRepository
	{
		private readonly IDbConnectionFactory _dbFactory;

		public CustomerRepository(IDbConnectionFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public async Task<IEnumerable<Customer>> GetAllAsync()
		{
			var query = "SELECT * FROM store.Customers";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QueryAsync<Customer>(query);
		}

		public async Task<Customer?> GetByIdAsync(Guid id)
		{
			var query = "SELECT * FROM store.Customers WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QueryFirstOrDefaultAsync<Customer>(query, new { Id = id });
		}

		public async Task<Customer?> CreateAsync(Customer customer)
		{
			var query = @"
				INSERT INTO store.Customers (Id, Email, FirstName, PhoneNumber, PurchaseDate)
				VALUES (@Id, @Email, @FirstName, @PhoneNumber, @PurchaseDate)";

			using var connection = _dbFactory.CreateConnection();

			customer.Id = Guid.NewGuid();
			customer.PurchaseDate = DateTime.Now;

			var rowAffected = await connection.ExecuteAsync(query, customer);

			return rowAffected > 0 ? customer : null;
		}

		public async Task<Customer?> UpdateAsync(Guid id, Customer customer)
		{
			var query = @"
				UPDATE store.Customers SET Email = @Email, FirstName = @FirstName, PhoneNumber = @PhoneNumber, PurchaseDate = @PuchaseDate
				WHERE Id = @Id";

			var connection = _dbFactory.CreateConnection();

			var rowAffected = await connection.ExecuteAsync(query, new { Id = id, customer.Email, customer.FirstName, customer.PhoneNumber, customer.PurchaseDate });

			return rowAffected > 0 ? customer : null;

		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var query = "DELETE FROM store.Customers WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var rowAffected = await connection.ExecuteAsync(query, new { Id = id });

			return rowAffected > 0;
		}
	}
}

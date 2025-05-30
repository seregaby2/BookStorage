using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly IDbConnectionFactory _dbFactory;

		public BookRepository(IDbConnectionFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public async Task<IEnumerable<Book>> GetAllAsync()
		{
			var query = "SELECT * FROM store.Books";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QueryAsync<Book>(query);

		}

		public async Task<Book?> GetByIdAsync(Guid id)
		{
			var query = "SELECT * FROM store.Books WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QueryFirstOrDefaultAsync<Book>(query, new { Id = id });
		}

		public async Task<Book?> CreateAsync(Book book)
		{
			var query = @"
				INSERT INTO store.Books (Id, Title, Genre, PublishDate, Price, AuthorId)
				VALUES (@Id, @Title, @Genre, @PublishDate, @Price, @AuthorId)";

			var connection = _dbFactory.CreateConnection();

			book.Id = Guid.NewGuid();

			book.PublishDate = DateTime.Now;

			var rowsAffected = await connection.ExecuteAsync(query, book);

			return rowsAffected > 0 ? book : null;

		}

		public async Task<Book?> UpdateAsync(Guid id, Book book)
		{
			var query = @"
				UPDATE store.Books SET Title = @Title, Genre = @Genre, PublishDate = @PublishDate, Price = @Price
				WHERE Id = @Id";

			var connection = _dbFactory.CreateConnection();

			var rowsAffected = await connection.ExecuteAsync(query, new { book.Title, book.Genre, book.PublishDate, book.Price, Id = id });

			return rowsAffected > 0 ? book : null;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var query = "DELETE * FROM store.Books WHERE Id = @Id";

			var connection = _dbFactory.CreateConnection();

			var rowsAffected = await connection.ExecuteAsync(query, new { ID = id });

			return rowsAffected > 0;

		}
	}
}

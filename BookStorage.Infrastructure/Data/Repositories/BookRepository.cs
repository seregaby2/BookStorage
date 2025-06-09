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
			const string query = @"
				SELECT b.*, a.Id, a.FirstName, a.LastName, a.BirthDate 
				FROM store.Books b
				JOIN store.Authors a ON b.AuthorId = a.Id";

			using var connection = _dbFactory.CreateConnection();

			var result = await connection.QueryAsync<Book, Author, Book>(
				query,
				(book, author) =>
				{
					book.Author = author;
					return book;
				},
				splitOn: "Id"
			);

			return result;
		}

		public async Task<Book?> GetByIdAsync(Guid id)
		{
			const string query = @"
				SELECT b.*, a.Id AS AuthorId, a.FirstName, a.LastName, a.BirthDate 
				FROM store.Books b
				JOIN store.Authors a ON b.AuthorId = a.Id
				WHERE b.Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var result = await connection.QueryAsync<Book, Author, Book>(
				query,
				(book, author) =>
				{
					book.Author = author;
					return book;
				},
				new { Id = id },
				splitOn: "AuthorId"
			);

			return result.FirstOrDefault();
		}

		public async Task<Book?> CreateAsync(Book book)
		{
			var query = @"
				INSERT INTO store.Books (Id, Title, Genre, PublishDate, Price, AuthorId)
				VALUES (@Id, @Title, @Genre, @PublishDate, @Price, @AuthorId)";

			using var connection = _dbFactory.CreateConnection();

			book.Id = Guid.NewGuid();

			book.PublishDate = DateTime.Now;

			var rowsAffected = await connection.ExecuteAsync(query, book);

			return rowsAffected > 0 ? book : null;
		}

		public async Task<Book?> UpdateAsync(Guid id, Book book)
		{
			var query = @"
				UPDATE store.Books SET Title = @Title, Genre = @Genre, Price = @Price, AuthorId = @AuthorId
				WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var rowsAffected = await connection.ExecuteAsync(query, new { book.Title, book.Genre, book.Price, book.AuthorId, Id = id });

			return rowsAffected > 0 ? book : null;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var query = "DELETE FROM store.Books WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

			return rowsAffected > 0;
		}
	}
}

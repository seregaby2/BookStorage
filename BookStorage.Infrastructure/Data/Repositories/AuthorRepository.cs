using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class AuthorRepository : IAuthorRepository
	{
		private readonly IDbConnectionFactory _dbFactory;

		public AuthorRepository(IDbConnectionFactory dbFactory)
		{
			_dbFactory = dbFactory;
		}

		public async Task<bool> CheckIfAuthorAlreadyExistsAsync(string firstName, string lastName, DateTime birthDate)
		{
			var query = @"
        SELECT 1 
        FROM store.Authors 
        WHERE FirstName = @FirstName AND LastName = @LastName AND BirthDate = @BirthDate";

			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();

			var result = await connection.ExecuteScalarAsync<object?>(query, new
			{
				FirstName = firstName,
				LastName = lastName,
				BirthDate = birthDate
			});

			return result != null;
		}

		public async Task<IEnumerable<Author>> GetAllAsync()
		{
			var query = "SELECT * FROM store.Authors";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QueryAsync<Author>(query);
		}

		public async Task<Author?> GetByIdAsync(Guid id)
		{
			var query = "SELECT * FROM store.Authors WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			return await connection.QuerySingleOrDefaultAsync<Author>(query, new { Id = id });
		}

		public async Task<Author?> CreateAsync(Author author)
		{
			var query = @"
                INSERT INTO store.Authors (Id, FirstName, LastName, BirthDate)
                VALUES (@Id, @FirstName, @LastName, @BirthDate)";

			using var connection = _dbFactory.CreateConnection();

			author.Id = Guid.NewGuid();

			var rowsAffected = await connection.ExecuteAsync(query, author);

			return rowsAffected > 0 ? author : null;
		}

		public async Task<Author?> UpdateAsync(Guid id, Author author)
		{
			var query = @"
                UPDATE store.Authors SET FirstName = @FirstName, LastName = @LastName, BirthDate = @BirthDate
                WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();

			var rowsAffected = await connection.ExecuteAsync(query, new { author.FirstName, author.LastName, author.BirthDate, Id = id });

			return rowsAffected > 0 ? author : null;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var booksQuery = "DELETE FROM store.Books WHERE AuthorId = @AuthorId";
			var authorQuery = "DELETE FROM store.Authors WHERE Id = @Id";

			using var connection = _dbFactory.CreateConnection();
			await connection.OpenAsync();

			using var transaction = connection.BeginTransaction();

			try
			{
				await connection.ExecuteAsync(booksQuery, new { AuthorId = id }, transaction);
				var rowsAffected = await connection.ExecuteAsync(authorQuery, new { Id = id }, transaction);

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

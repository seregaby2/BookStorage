using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Dapper;

namespace BookStorage.Infrastructure.Data.Repositories
{
	public class UserRepository : IUserRepository
	{
		private readonly IDbConnectionFactory _connectionFactory;

		public UserRepository(IDbConnectionFactory connectionFactory) => _connectionFactory = connectionFactory;

		public async Task<User?> GetByEmailAsync(string email)
		{
			var query = "SELECT * FROM Store.Users WHERE Email = @Email";

			using var connection = _connectionFactory.CreateConnection();

			return await connection.QueryFirstOrDefaultAsync<User>(query, new { Email = email });
		}

		public async Task CreateAsync(User user)
		{
			var query = @"INSERT INTO Store.Users (Id, Email, PasswordHash, Role)
						  VALUES (@Id, @Email, @PasswordHash, @Role)";

			using var connection = _connectionFactory.CreateConnection();

			await connection.ExecuteAsync(query, user);
		}
	}
}

using BookStorage.Infrastructure.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data.Common;

namespace BookStorage.Infrastructure.Data
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
		private readonly string _connectionString;

		public DbConnectionFactory(IConfiguration configuration)
		{
			_connectionString = configuration.GetConnectionString("DefaultConnection")!;
		}

		public DbConnection CreateConnection() => new SqlConnection(_connectionString);
	}
}

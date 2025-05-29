using System.Data.Common;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IDbConnectionFactory
	{
		DbConnection CreateConnection();
	}
}

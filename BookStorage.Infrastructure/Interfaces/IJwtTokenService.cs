using BookStorage.Domain.Models;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IJwtTokenService
	{
		string GenerateToken(User user);
	}
}

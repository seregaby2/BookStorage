using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IUserService
	{
		Task<User?> GetByEmailAsync(string email);
		Task CreateAsync(User user);

	}
}

using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _userRepository.GetByEmailAsync(email);
		}

		public async Task CreateAsync(User user)
		{
			await _userRepository.CreateAsync(user);
		}
	}
}

using BookStorage.Domain.Models;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IAuthorRepository
	{
		Task<IEnumerable<Author>> GetAllAsync();
		Task<Author?> GetByIdAsync(Guid id);
		Task<Author?> CreateAsync(Author author);
		Task<Author?> UpdateAsync(Guid id, Author author);
		Task<bool> DeleteAsync(Guid id);
		Task<bool> CheckIfAuthorAlreadyExistsAsync(string authorFirstName, string authorLastName, DateTime birthDate);
	}
}

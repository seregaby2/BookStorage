using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IAuthorService
	{
		Task<IEnumerable<Author>> GetAll();
		Task<Author?> GetById(Guid id);
		Task<Author?> Create(Author author);
		Task<Author?> Update(Guid id, Author author);
		Task<bool> Delete(Guid id);
	}
}

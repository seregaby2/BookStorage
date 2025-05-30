using BookStorage.Domain.Models;

namespace BookStorage.Infrastructure.Interfaces
{
	public interface IBookRepository
	{
		Task<IEnumerable<Book>> GetAllAsync();
		Task<Book?> GetByIdAsync(Guid id);
		Task<Book?> CreateAsync(Book book);
		Task<Book?> UpdateAsync(Guid id, Book book);
		Task<bool> DeleteAsync(Guid id);
		//void DeleteByAuthorId(Guid authorId);
	}
}

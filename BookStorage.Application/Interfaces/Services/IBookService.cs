
using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface IBookService
	{
		Task<IEnumerable<Book>> GetAll();
		Task<Book?> GetById(Guid id);
		Task<Book?> Create(Book book);
		Task<Book?> Update(Guid id, Book book);
		Task<bool> Delete(Guid id);
	}
}

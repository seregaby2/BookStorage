using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _repository;


		public BookService(IBookRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Book>> GetAll()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Book?> GetById(Guid id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Book?> Create(Book book)
		{
			//var author = _authorService.GetById(book.AuthorID);
			//if (author == null)
			//	return null;

			return await _repository.CreateAsync(book);
		}

		public async Task<Book?> Update(Guid id, Book book)
		{
			//var author = _authorService.GetById(book.AuthorID);
			//if (author == null)
			//	return null;

			var existingBook = await _repository.GetByIdAsync(id);
			if (existingBook == null)
				return null;


			return await _repository.UpdateAsync(id, book);
		}

		public async Task<bool> Delete(Guid id)
		{
			var existingBook = await _repository.GetByIdAsync(id);
			if (existingBook == null)
				return false;

			return await _repository.DeleteAsync(id);
		}

	}
}

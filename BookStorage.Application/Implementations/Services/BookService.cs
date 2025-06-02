using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class BookService : IBookService
	{
		private readonly IBookRepository _bookRepository;
		private readonly IAuthorRepository _authorRepository;


		public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
		{
			_bookRepository = bookRepository;
			_authorRepository = authorRepository;
		}

		public async Task<IEnumerable<Book>> GetAll()
		{
			return await _bookRepository.GetAllAsync();
		}

		public async Task<Book?> GetById(Guid id)
		{
			return await _bookRepository.GetByIdAsync(id);
		}

		public async Task<Book?> Create(Book book)
		{
			var author = _authorRepository.GetByIdAsync(book.AuthorId);
			if (author == null)
				return null;

			return await _bookRepository.CreateAsync(book);
		}

		public async Task<Book?> Update(Guid id, Book book)
		{
			var author = _authorRepository.GetByIdAsync(book.AuthorId);
			if (author == null)
				return null;

			var existingBook = await _bookRepository.GetByIdAsync(id);
			if (existingBook == null)
				return null;


			return await _bookRepository.UpdateAsync(id, book);
		}

		public async Task<bool> Delete(Guid id)
		{
			var existingBook = await _bookRepository.GetByIdAsync(id);
			if (existingBook == null)
				return false;

			return await _bookRepository.DeleteAsync(id);
		}

	}
}

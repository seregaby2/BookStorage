using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Enums;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
	public class BookService : IBookService
	{
		private readonly IAuthorService _authorService;
		private static readonly List<Book> Books =
		[
			new()
			{
				Id = Guid.NewGuid(),
				Title = "White Fang",
				Genre = BookGenre.Adventure,
				Price = 100,
				PublishDate = new DateTime(1906,10,1),
				Author = new Author(),
				AuthorID = Guid.NewGuid()
			},
			new()
			{
				Id = Guid.NewGuid(),
				Title = "War and Peace",
				Genre = BookGenre.Novel,
				Price = 150,
				PublishDate = new DateTime(1869,12,1),
				Author = new Author(),
				AuthorID = Guid.NewGuid()
			}
		];

		public BookService(IAuthorService authorService)
		{
			_authorService = authorService;
		}

		public IEnumerable<Book> GetAll()
		{
			return Books;
		}

		public Book? GetById(Guid id)
		{
			var book = Books.FirstOrDefault(a => a.Id == id);

			return book;
		}

		public Book? Create(Book book)
		{
			var author = _authorService.GetById(book.AuthorID);
			if (author == null)
				return null;

			book.Id = Guid.NewGuid();

			Books.Add(book);

			return book;
		}

		public Book? Update(Guid id, Book book)
		{
			var author = _authorService.GetById(book.AuthorID);
			if (author == null)
				return null;

			var existingBook = Books.FirstOrDefault(a => a.Id == id);
			if (existingBook == null)
				return null;

			existingBook.Title = book.Title;
			existingBook.Price = book.Price;
			existingBook.Genre = book.Genre;

			return existingBook;
		}

		public bool Delete(Guid id)
		{
			var bookToDelete = Books.FirstOrDefault(a => a.Id == id);
			if (bookToDelete == null)
				return false;

			return Books.Remove(bookToDelete);
		}

		public void DeleteByAuthorId(Guid authorId)
		{
			Books.RemoveAll(book => book.AuthorID == authorId);

		}
	}
}

using BookStorage.Application.Interfaces.Services;

namespace BookStorage.Application.Implementations.Services
{
	public class AuthorBookService : IAuthorBookService
	{
		private readonly IAuthorService _authorService;
		private readonly IBookService _bookService;

		public AuthorBookService(IAuthorService authorService, IBookService bookService)
		{
			_authorService = authorService;
			_bookService = bookService;
		}

		public bool DeleteAuthorAndBooks(Guid authorId)
		{
			var authorExists = _authorService.GetById(authorId);
			if (authorExists == null)
				return false;

			_bookService.DeleteByAuthorId(authorId);

			return _authorService.Delete(authorId);
		}
	}
}

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
			bool result = _authorService.Delete(authorId);

			if (result)
				_bookService.Delete(authorId);

			return result;
		}
	}
}

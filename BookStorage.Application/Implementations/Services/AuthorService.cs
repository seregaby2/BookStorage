using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
	public class AuthorService : IAuthorService
	{
		private static readonly List<Author> Authors =
		[
			new()
			{
				Id = Guid.NewGuid(),
				FirstName = "Jack",
				LastName = "London",
				BirthDate = new DateTime(1876, 12, 1),
				Books = new List<Book>()
			},
			new()
			{
				Id = Guid.NewGuid(),
				FirstName = "Leo",
				LastName = "Tolstoy",
				BirthDate = new DateTime(1910, 11, 20),
				Books = new List<Book>()
			}
		];

		public IEnumerable<Author> GetAll()
		{
			return Authors;
		}

		public Author? GetById(Guid id)
		{
			var author = Authors.FirstOrDefault(a => a.Id == id);

			return author;
		}

		public Author? Create(Author author)
		{
			bool exists = Authors.Any(a =>
				string.Equals(a.FirstName, author.FirstName, StringComparison.OrdinalIgnoreCase) &&
				string.Equals(a.LastName, author.LastName, StringComparison.OrdinalIgnoreCase));

			if (exists)
				return null;

			Authors.Add(author);

			return author;
		}

		public Author? Update(Guid id, Author author)
		{
			var existingAuthor = Authors.FirstOrDefault(a => a.Id == id);
			if (existingAuthor == null)
				return null;

			existingAuthor.FirstName = author.FirstName;
			existingAuthor.LastName = author.LastName;
			existingAuthor.BirthDate = author.BirthDate;

			return existingAuthor;
		}

		public bool Delete(Guid id)
		{
			var authorToDelete = Authors.FirstOrDefault(a => a.Id == id);
			if (authorToDelete == null)
				return false;

			bool wasDeletedAuthor = Authors.Remove(authorToDelete);

			return wasDeletedAuthor;
		}
	}
}

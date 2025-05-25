using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class AuthorService : IAuthorService
    {
        public static readonly List<Author> _authors = new List<Author>
        {
            new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                LastName = "London",
                BirthDate = new DateTime(1876, 12, 1),
                Books = new List<Book>()
            },
            new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Leo",
                LastName = "Tolstoy",
                BirthDate = new DateTime(1910, 11, 20),
                Books = new List<Book>()
            }
        };

        public IEnumerable<Author> GetAll()
        {
            return _authors;
        }

        public Author? GetById(Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
                return null;

            return author;
        }

        public Author Create(Author author)
        {
            author.Id = Guid.NewGuid();
            author.Books = new List<Book>();

            _authors.Add(author);

            return author;
        }

        public Author? Update(Guid id, Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null)
                return null;

            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            existingAuthor.BirthDate = author.BirthDate;

            return existingAuthor;
        }

        public  Author? Delete(Guid id)
        {
            var authorToDelete = _authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
                return null;

            _authors.Remove(authorToDelete);

            return authorToDelete;
        }
    }
}

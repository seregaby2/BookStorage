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

        public Author Create(Author author)
        {
            author.Id = Guid.NewGuid();
            author.Books = new List<Book>();

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

        public  Author? Delete(Guid id)
        {
            var authorToDelete = Authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
                return null;

            Authors.Remove(authorToDelete);

            return authorToDelete;
        }
    }
}

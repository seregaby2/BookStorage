using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class BookService : IBookService
    {
        public static readonly List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "White Fang",
                Genre = "Adventure",
                Price = 100,
                PublishDate = new DateTime(1906,10,1),
                Author = new Author(),
                AuthrID = Guid.NewGuid()
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "War and Peace",
                Genre = "Novel",
                Price = 150,
                PublishDate = new DateTime(1869,12,1),
                Author = new Author(),
                AuthrID = Guid.NewGuid()
            }
        };

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public Book? GetById(Guid id)
        {
            var book = _books.FirstOrDefault(a => a.Id == id);
            if (book == null)
                return null;

            return book;
        }

        public Book Create(Book book)
        {
            book.Id = Guid.NewGuid();

            _books.Add(book);

            return book;
        }

        public Book? Update(Guid id, Book book)
        {
            var existingBook = _books.FirstOrDefault(a => a.Id == id);
            if (existingBook == null)
                return null;

            existingBook.AuthrID = book.AuthrID;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Price = book.Price;
            existingBook.Genre = book.Genre;

            return existingBook;
        }

        public Book? Delete(Guid id)
        {
            var bookToDelete = _books.FirstOrDefault(a => a.Id == id);
            if (bookToDelete == null)
                return null;

            _books.Remove(bookToDelete);

            return bookToDelete;
        }
    }
}

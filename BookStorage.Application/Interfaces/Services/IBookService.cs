
using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Book? GetById(Guid id);
        Book Create(Book book);
        Book? Update(Guid id, Book book);
        Book? Delete(Guid id);
    }
}

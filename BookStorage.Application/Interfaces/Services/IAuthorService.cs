using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Author? GetById(Guid id);
        Author Create(Author author);
        Author? Update(Guid id, Author author);
        Author? Delete(Guid id);
    }
}

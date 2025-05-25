using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAll();
        Customer? GetById(Guid id);
        Customer Create(Customer customer);
        Customer? Update(Guid id, Customer customer);
        Customer? Delete(Guid id);
    }
}

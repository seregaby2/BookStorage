using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
	public interface ICustomerService
	{
		Task<IEnumerable<Customer>> GetAll();
		Task<Customer?> GetById(Guid id);
		Task<Customer?> Create(Customer customer);
		Task<Customer?> Update(Guid id, Customer customer);
		Task<bool> Delete(Guid id);
	}
}

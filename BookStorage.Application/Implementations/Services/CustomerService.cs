using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _repository;

		public CustomerService(ICustomerRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Customer>> GetAll()
		{
			return await _repository.GetAllAsync();
		}

		public async Task<Customer?> GetById(Guid id)
		{
			return await _repository.GetByIdAsync(id);
		}

		public async Task<Customer?> Create(Customer customer)
		{
			//bool exists = Customers.Any(a =>
			//    string.Equals(a.Email, customer.Email, StringComparison.OrdinalIgnoreCase));
			//if (exists)
			//    return null;


			return await _repository.CreateAsync(customer);
		}

		public async Task<Customer?> Update(Guid id, Customer customer)
		{
			//var existingCustomer = Customers.FirstOrDefault(a => a.Id == id);
			//if (existingCustomer == null)
			//	return null;

			return await _repository.UpdateAsync(id, customer);
		}

		public async Task<bool> Delete(Guid id)
		{
			//var customerToDelete = Customers.FirstOrDefault(a => a.Id == id);
			//if (customerToDelete == null)
			//	return false;

			return await _repository.DeleteAsync(id);
		}
	}
}

using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.Application.Implementations.Services
{
	public class CustomerService : ICustomerService
	{
		private readonly ICustomerRepository _customerRepository;

		public CustomerService(ICustomerRepository customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public async Task<IEnumerable<Customer>> GetAll()
		{
			return await _customerRepository.GetAllAsync();
		}

		public async Task<Customer?> GetById(Guid id)
		{
			return await _customerRepository.GetByIdAsync(id);
		}

		public async Task<Customer?> Create(Customer customer)
		{
			var customers = await _customerRepository.GetAllAsync();

			bool exists = customers.Any(a =>
				string.Equals(a.Email, customer.Email, StringComparison.OrdinalIgnoreCase));
			if (exists)
				return null;

			return await _customerRepository.CreateAsync(customer);
		}

		public async Task<Customer?> Update(Guid id, Customer customer)
		{
			var customerToDelete = _customerRepository.GetByIdAsync(id);
			if (customerToDelete == null)
				return null;

			return await _customerRepository.UpdateAsync(id, customer);
		}

		public async Task<bool> Delete(Guid id)
		{
			var customerToDelete = _customerRepository.GetByIdAsync(id);
			if (customerToDelete == null)
				return false;

			return await _customerRepository.DeleteAsync(id);
		}
	}
}

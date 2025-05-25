using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class CustomerService : ICustomerService
    {
        public static readonly List<Customer> _customers = new List<Customer>
        {
            new Customer
          {
              Id = Guid.NewGuid(),
              Email  = "s.@gmail.com",
              Name  = "Alex",
              PhoneNumber  = "+375297777777",
              PurchasehDate = new DateTime(),
              Orders = new List<Order>()
          },
          new Customer
          {
              Id = Guid.NewGuid(),
              Email  = "v.@gmail.com",
              Name  = "Mark",
              PhoneNumber  = "+375291111111",
              PurchasehDate = new DateTime(),
              Orders = new List<Order>()
          },
        };

        public IEnumerable<Customer> GetAll()
        {
            return _customers;
        }

        public Customer? GetById(Guid id)
        {
            var customer = _customers.FirstOrDefault(a => a.Id == id);
            if (customer == null)
                return null;

            return customer;
        }

        public Customer Create(Customer customer)
        {
            customer.Id = Guid.NewGuid();

            _customers.Add(customer);

            return customer;
        }

        public Customer? Update(Guid id, Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(a => a.Id == id);
            if (existingCustomer == null)
                return null;

            existingCustomer.Email = customer.Email;
            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.PurchasehDate = customer.PurchasehDate;

            return existingCustomer;
        }

        public Customer? Delete(Guid id)
        {
            var customerToDelete = _customers.FirstOrDefault(a => a.Id == id);
            if (customerToDelete == null)
                return null;
            
            _customers.Remove(customerToDelete);

            return customerToDelete;
        }
    }
}

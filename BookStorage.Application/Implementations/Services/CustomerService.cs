using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class CustomerService : ICustomerService
    {
        private static readonly List<Customer> Customers =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Email  = "s.@gmail.com",
                Name  = "Alex",
                PhoneNumber  = "+375297777777",
                PurchasehDate = new DateTime(),
                Orders = new List<Order>()
            },
            new()
            {
                Id = Guid.NewGuid(),
                Email  = "v.@gmail.com",
                Name  = "Mark",
                PhoneNumber  = "+375291111111",
                PurchasehDate = new DateTime(),
                Orders = new List<Order>()
            }
        ];

        public IEnumerable<Customer> GetAll()
        {
            return Customers;
        }

        public Customer? GetById(Guid id)
        {
            var customer = Customers.FirstOrDefault(a => a.Id == id);

            return customer;
        }

        public Customer Create(Customer customer)
        {
            customer.Id = Guid.NewGuid();

            Customers.Add(customer);

            return customer;
        }

        public Customer? Update(Guid id, Customer customer)
        {
            var existingCustomer = Customers.FirstOrDefault(a => a.Id == id);
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
            var customerToDelete = Customers.FirstOrDefault(a => a.Id == id);
            if (customerToDelete == null)
                return null;

            Customers.Remove(customerToDelete);

            return customerToDelete;
        }
    }
}

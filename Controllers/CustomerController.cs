using BookStorage.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
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

      [HttpGet]
      public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_customers);
        }

        [HttpGet("{id}")]
        public ActionResult<Customer> GetById([FromRoute] Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            _customers.Add(customer);
            return Ok(customer);
        }

        [HttpPut("{id}")]
        public ActionResult<Customer> Update([FromRoute] Guid id, [FromBody] Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(x=>x.Id == id);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            existingCustomer.Name = customer.Name;
            existingCustomer.PhoneNumber = customer.PhoneNumber;
            existingCustomer.Email = customer.Email;
            existingCustomer.PurchasehDate = customer.PurchasehDate;
            return Ok(existingCustomer);
        }
        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete([FromRoute] Guid id)
        {
            var customer = _customers.FirstOrDefault(x=>x.Id==id);
            if (customer == null)
            {
                return NotFound();
            }
            _customers.Remove(customer);
            return Ok(customer);
        }

    }
      }
        

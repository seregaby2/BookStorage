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

        /// <summary>
        /// Retrieves all customers available in the system
        /// </summary>
        /// <returns>A list of customers</returns>
        /// <response code="200">Returns the list of customers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Customer>), 200)]
        public ActionResult<IEnumerable<Customer>> GetAll()
        {
            return Ok(_customers);
        }

        /// <summary>
        /// Retrieves a specific customer by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the customer</param>
        /// <returns>The customer with the specified ID</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Customer> GetById([FromRoute] Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        /// <summary>
        /// Creates a new customer
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Customer
        ///     {
        ///         "Email" : "v.@gmail.com",
        ///         "Name" : "Mark",
        ///         "PhoneNumber" : "+375291111111",
        ///     }
        /// </remarks>
        /// <param name="customer">The customer to create</param>
        /// <returns>The newly created customer</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(Customer), 201)]
        [ProducesResponseType(400)]
        public ActionResult<Customer> Create([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _customers.Add(customer);
            return StatusCode(201, customer); ;
        }

        /// <summary>
        /// Updates an existing customer by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the customer to update</param>
        /// <param name="customer">The updated customer data</param>
        /// <returns>The updated customer</returns>
        /// <response code="200">Returns the updated customer</response>
        /// <response code="404">If the customer with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Customer), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Customer> Update([FromRoute] Guid id, [FromBody] Customer customer)
        {
            var existingCustomer = _customers.FirstOrDefault(x => x.Id == id);
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

        /// <summary>
        /// Deletes an customer by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the customer to delete</param>
        /// <response code="204">Customer was successfully deleted</response>
        /// <response code="404">Customer with the specified ID was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            _customers.Remove(customer);
            return NoContent();
        }
    }
}
        

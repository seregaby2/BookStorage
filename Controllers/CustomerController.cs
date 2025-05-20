using BookStorage.DTOs.Book;
using BookStorage.DTOs.Customer;
using BookStorage.Models;
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
        [ProducesResponseType(typeof(IEnumerable<CustomerViewDto>), 200)]
        public ActionResult<IEnumerable<CustomerViewDto>> GetAll()
        {
            var customerDto = _customers.Select(a => new CustomerViewDto
            {
                Id = a.Id,
                Email = a.Email,
                Name = a.Name,
                PhoneNumber = a.PhoneNumber,
                PurchasehDate = a.PurchasehDate,
            });
            return Ok(customerDto);
        }

        /// <summary>
        /// Retrieves a specific customer by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the customer</param>
        /// <returns>The customer with the specified ID</returns>
        /// <response code="200">Returns the customer</response>
        /// <response code="404">If the customer is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<CustomerViewDto> GetById([FromRoute] Guid id)
        {
            var customer = _customers.FirstOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            var customerDto = new CustomerViewDto
            {
                Id = customer.Id,
                Email = customer.Email,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                PurchasehDate = customer.PurchasehDate,
            };

            return Ok(customerDto);
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
        /// <param name="customerDto">The customer to create</param>
        /// <returns>The newly created customer</returns>
        /// <response code="201">Returns the newly created customer</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(CustomerViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<CustomerViewDto> Create([FromBody] CreateCustomerDto customerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                Email = customerDto.Email,
                Name = customerDto.Name,
                PhoneNumber = customerDto.PhoneNumber,
                PurchasehDate = customerDto.PurchasehDate,
                Orders = new List<Order>()
            };

            _customers.Add(customer);

            var createdDto = new CustomerViewDto
            {
                Id = Guid.NewGuid(),
                Email = customer.Email,
                Name = customer.Name,
                PhoneNumber = customer.PhoneNumber,
                PurchasehDate = customer.PurchasehDate,
            };

            return StatusCode(201, createdDto); ;
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
        [ProducesResponseType(typeof(CustomerViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<CustomerViewDto> Update([FromRoute] Guid id, [FromBody] UpdateCustomerDto customer)
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

            var updatedDto = new CustomerViewDto
            {
                Id = existingCustomer.Id,
                Email = existingCustomer.Email,
                Name = existingCustomer.Name,
                PhoneNumber = existingCustomer.PhoneNumber,
                PurchasehDate = existingCustomer.PurchasehDate,
            };
            
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
        

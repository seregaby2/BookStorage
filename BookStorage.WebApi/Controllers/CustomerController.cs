using BookStorage.WebApi.DTOs.Customer;
using Microsoft.AspNetCore.Mvc;
using BookStorage.Domain.Models;
using AutoMapper;
using BookStorage.Application.Interfaces.Services;

namespace BookStorage.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        public CustomerController(IMapper mapper, ICustomerService customerService)
        {
            _mapper = mapper;
            _customerService = customerService;
        }

        /// <summary>
        /// Retrieves all customers available in the system
        /// </summary>
        /// <returns>A list of customers</returns>
        /// <response code="200">Returns the list of customers.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CustomerViewDto>), 200)]
        public ActionResult<IEnumerable<CustomerViewDto>> GetAll()
        {
            var customers = _customerService.GetAll();

            var customerDto = _mapper.Map<List<CustomerViewDto>>(customers);

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
            var customer = _customerService.GetById(id);
            if (customer == null)
                return NotFound();

            var customerDto = _mapper.Map<CustomerViewDto>(customer);
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
                return BadRequest(ModelState);

            var customer = _mapper.Map<Customer>(customerDto);

            var createdCustomer = _customerService.Create(customer);

            var createdDto = _mapper.Map<CustomerViewDto>(createdCustomer);

            return StatusCode(201, createdDto);
        }

        /// <summary>
        /// Updates an existing customer by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the customer to update</param>
        /// <param name="customerDto">The updated customer data</param>
        /// <returns>The updated customer</returns>
        /// <response code="200">Returns the updated customer</response>
        /// <response code="404">If the customer with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CustomerViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<CustomerViewDto> Update([FromRoute] Guid id, [FromBody] UpdateCustomerDto customerDto)
        {
            var existingCustomer = _customerService.GetById(id);
            if (existingCustomer == null)
                return NotFound();

            var customerToUpdate = _mapper.Map<Customer>(customerDto);

            var updatedCustomer = _customerService.Update(id, customerToUpdate);

            var customerViewDto = _mapper.Map<CustomerViewDto>(updatedCustomer);

            return Ok(customerViewDto);
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
            var customerToDelete = _customerService.Delete(id);
            if (customerToDelete == null)
                return NotFound();

            return NoContent();
        }
    }
}
        

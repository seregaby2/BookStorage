using AutoMapper;
using BookStorage.Application.Mediatr.CustomerMediatr.CreateCustomer;
using BookStorage.Application.Mediatr.CustomerMediatr.DeleteCustomer;
using BookStorage.Application.Mediatr.CustomerMediatr.GetAllCustomers;
using BookStorage.Application.Mediatr.CustomerMediatr.GetByIdCustomer;
using BookStorage.Application.Mediatr.CustomerMediatr.UpdateCustomer;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Customer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		public CustomerController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		/// <summary>
		/// Retrieves all customers available in the system
		/// </summary>
		/// <returns>A list of customers</returns>
		/// <response code="200">Returns the list of customers.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<CustomerViewDto>), 200)]
		public async Task<ActionResult<IEnumerable<CustomerViewDto>>> GetAll()
		{
			var customers = await _mediator.Send(new GetAllCustomersQuery());

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
		public async Task<ActionResult<CustomerViewDto>> GetById([FromRoute] Guid id)
		{
			var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
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
		///         "firstName" : "Mark",
		///         "PhoneNumber" : "+375291111111",
		///			"purchaseDate": "2025-05-31T15:02:11.540Z"
		///     }
		/// </remarks>
		/// <param name="customerDto">The customer to create</param>
		/// <returns>The newly created customer</returns>
		/// <response code="201">Returns the newly created customer</response>
		/// <response code="400">If the input model is invalid</response>
		[HttpPost]
		[ProducesResponseType(typeof(CustomerViewDto), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<CustomerViewDto>> Create([FromBody] CreateCustomerDto customerDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var customer = _mapper.Map<Customer>(customerDto);

			var createdCustomer = await _mediator.Send(new CreateCustomerCommand(customer));
			if (createdCustomer == null)
				return BadRequest("Author with the same email already exists.");

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
		public async Task<ActionResult<CustomerViewDto>> Update([FromRoute] Guid id, [FromBody] UpdateCustomerDto customerDto)
		{
			var customerToUpdate = _mapper.Map<Customer>(customerDto);

			var updatedCustomer = await _mediator.Send(new UpdateCustomerCommand(id, customerToUpdate));
			if (updatedCustomer == null)
				return NotFound();

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
		public async Task<ActionResult> Delete([FromRoute] Guid id)
		{
			var customerToDelete = await _mediator.Send(new DeleteCustomerCommand(id));
			if (!customerToDelete)
				return NotFound();

			return NoContent();
		}
	}
}


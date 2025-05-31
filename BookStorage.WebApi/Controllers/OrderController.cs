using AutoMapper;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Order;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class OrderController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IOrderService _orderService;
		public OrderController(IMapper mapper, IOrderService orderService)
		{
			_mapper = mapper;
			_orderService = orderService;
		}

		/// <summary>
		/// Retrieves all orders available in the system
		/// </summary>
		/// <returns>A list of orders</returns>
		/// <response code="200">Returns the list of orders.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<OrderViewDto>), 200)]
		public async Task<ActionResult<IEnumerable<OrderViewDto>>> GetAll()
		{
			var orders = await _orderService.GetAll();

			var ordersDto = _mapper.Map<List<OrderViewDto>>(orders);

			return Ok(ordersDto);
		}

		/// <summary>
		/// Retrieves a specific order by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the order</param>
		/// <returns>The order with the specified ID</returns>
		/// <response code="200">Returns the order</response>
		/// <response code="404">If the author is not found</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(OrderViewDto), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<OrderViewDto>> GetById([FromRoute] Guid id)
		{
			var order = await _orderService.GetById(id);
			if (order == null)
				return NotFound();

			var orderDto = _mapper.Map<OrderViewDto>(order);

			return Ok(orderDto);
		}

		/// <summary>
		/// Creates a new order
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /api/Order
		///     {
		///		"customerId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
		///		"status": "Pending",
		///		"books": [
		///			{
		///			"bookId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
		///			"quantity": 1
		///			}
		///				]
		///		}
		/// </remarks>
		/// <param name="orderDto">The order to create</param>
		/// <returns>The newly created order</returns>
		/// <response code="201">Returns the newly created order</response>
		/// <response code="400">If the input model is invalid</response>
		[HttpPost]
		[ProducesResponseType(typeof(OrderViewDto), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<OrderViewDto>> Create([FromBody] CreateOrderDto orderDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var order = _mapper.Map<Order>(orderDto);

			var createdOrder = await _orderService.Create(order);

			if (createdOrder == null)
				return BadRequest("CustomerId or BookId are not found");

			var createdDto = _mapper.Map<OrderViewDto>(createdOrder);

			return StatusCode(201, createdDto);
		}

		/// <summary>
		/// Updates an existing order by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the order to update</param>
		/// <param name="orderDto">The updated order data</param>
		/// <returns>The updated order</returns>
		/// <response code="200">Returns the updated order</response>
		/// <response code="404">If the order with the specified ID is not found</response>
		[HttpPut("{id}")]
		[ProducesResponseType(200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<Order>> Update([FromRoute] Guid id, [FromBody] UpdateOrderDto orderDto)
		{
			var orderToUpdate = _mapper.Map<Order>(orderDto);

			var updatedOrder = await _orderService.Update(id, orderToUpdate);
			if (updatedOrder is null)
				return NotFound("CustomerId or BookId are not found");

			if (updatedOrder == false)
				return BadRequest("Failed to update order");

			return Ok("Order was updated");
		}

		/// <summary>
		/// Deletes an order by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the order to delete</param>
		/// <response code="204">Order was successfully deleted</response>
		/// <response code="404">Order with the specified ID was not found</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete([FromRoute] Guid id)
		{
			var orderToDelete = await _orderService.Delete(id);
			if (!orderToDelete)
				return NotFound();

			return NoContent();
		}
	}
}


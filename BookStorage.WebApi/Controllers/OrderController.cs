using BookStorage.WebApi.DTOs.Order;
using Microsoft.AspNetCore.Mvc;
using BookStorage.Domain.Models;
using AutoMapper;
using BookStorage.Application.Interfaces.Services;

namespace BookStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public ActionResult<IEnumerable<OrderViewDto>> GetAll()
        {
            var orders = _orderService.GetAll();

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
        public ActionResult<OrderViewDto> GetById([FromRoute] Guid id)
        {
            var order = _orderService.GetById(id);
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
        ///         "TotalAmount"  : "500",
        ///     }
        /// </remarks>
        /// <param name="orderDto">The order to create</param>
        /// <returns>The newly created order</returns>
        /// <response code="201">Returns the newly created order</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<OrderViewDto> Create([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<Order>(orderDto);

            var createdOrder = _orderService.Create(order);

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
        [ProducesResponseType(typeof(OrderViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderViewDto> Update([FromRoute] Guid id, [FromBody] UpdateOrderDto orderDto)
        {
            var existingOrder = _orderService.GetById(id);
            if (existingOrder == null)
                return NotFound();

            var orderToUpdate = _mapper.Map<Order>(orderDto);

            var updatedOrder = _orderService.Update(id, orderToUpdate);

            var updatedDto = _mapper.Map<OrderViewDto>(updatedOrder);
            
            return Ok(updatedDto);
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
        public ActionResult Delete([FromRoute] Guid id)
        {
            var orderToDelete = _orderService.Delete(id);
            if (orderToDelete == null)
                return NotFound();

            return NoContent();
        }
    }
}


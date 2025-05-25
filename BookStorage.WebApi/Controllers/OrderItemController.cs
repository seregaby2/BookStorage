using BookStorage.WebApi.DTOs.OrderItem;
using Microsoft.AspNetCore.Mvc;
using BookStorage.Domain.Models;
using AutoMapper;
using BookStorage.Application.Interfaces.Services;

namespace BookStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IMapper mapper, IOrderItemService orderItemService)
        {
            _mapper = mapper;
            _orderItemService = orderItemService;
        }

        /// <summary>
        /// Retrieves all orderItems available in the system
        /// </summary>
        /// <returns>A list of orderItems</returns>
        /// <response code="200">Returns the list of orderItems.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderItemViewDto>), 200)]
        public ActionResult<IEnumerable<OrderItem>> GetAll()
        {
            var orderItems = _orderItemService.GetAll();

            var orderItemsDto = _mapper.Map<List<OrderItemViewDto>>(orderItems);

            return Ok(orderItems);
        }

        /// <summary>
        /// Retrieves a specific orderItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem</param>
        /// <returns>The orderItem with the specified ID</returns>
        /// <response code="200">Returns the orderItem</response>
        /// <response code="404">If the orderItem is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderItemViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderItemViewDto> GetById([FromRoute] Guid id)
        {
            var orderItem = _orderItemService.GetById(id);
            if (orderItem == null)
                return NotFound();

            var orderItemDto = _mapper.Map<OrderItemViewDto>(orderItem);

            return Ok(orderItemDto);
        }

        /// <summary>
        /// Creates a new orderItem
        /// </summary>
        /// <param name="orderItemDto">The orderItem to create</param>
        /// <returns>The newly created orderItem</returns>
        /// <response code="201">Returns the newly created orderItem</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderItemViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<OrderItemViewDto> Create([FromBody] CreateOrderItemDto orderItemDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderItem = _mapper.Map<OrderItem>(orderItemDto);

            var createdOrderItem = _orderItemService.Create(orderItem);

            var createdDto = _mapper.Map<OrderItemViewDto>(orderItem);

            return StatusCode(201, createdDto);
        }

        /// <summary>
        /// Updates an existing orderItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to update</param>
        /// <param name="orderItemDto">The updated orderItem data</param>
        /// <returns>The updated orderItem</returns>
        /// <response code="200">Returns the updated orderItem</response>
        /// <response code="404">If the orderItem with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderItemViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderItemViewDto> Update([FromRoute] Guid id, [FromBody] UpdateOrderItemDto orderItemDto)
        {
            var existingOrderItem = _orderItemService.GetById(id);
            if (existingOrderItem == null)
                return NotFound();

            var orderItemToUpdate = _mapper.Map<OrderItem>(orderItemDto);

            var updatedOrderItem = _orderItemService.Update(id, orderItemToUpdate);

            var updatedDto = _mapper.Map<OrderItemViewDto>(updatedOrderItem);

            return Ok(updatedDto);
        }

        /// <summary>
        /// Deletes an orderItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to delete</param>
        /// <response code="204">OrderItem was successfully deleted</response>
        /// <response code="404">OrderItem with the specified ID was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var orderItemToDelete = _orderItemService.Delete(id);
            if (orderItemToDelete == null)
                return NotFound();

            return NoContent();
        }
    }
}

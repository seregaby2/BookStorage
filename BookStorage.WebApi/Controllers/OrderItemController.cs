using BookStorage.WebApi.DTOs.OrderItem;
using Microsoft.AspNetCore.Mvc;
using BookStorage.Domain.Models;
using AutoMapper;

namespace BookStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {

        private readonly IMapper _mapper;

        public OrderItemController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static readonly List<OrderItem> _orderItems = new List<OrderItem>
        {
          new OrderItem
          {
              Id = Guid.NewGuid(),
              OrderId  = Guid.NewGuid(),
              Order = new Order(),
              BookId = Guid.NewGuid(),
              Book = new Book()
          },
          new OrderItem
          {
              Id = Guid.NewGuid(),
              OrderId  = Guid.NewGuid(),
              Order = new Order(),
              BookId = Guid.NewGuid(),
              Book = new Book()
          },
        };

        /// <summary>
        /// Retrieves all orderItems available in the system
        /// </summary>
        /// <returns>A list of orderItems</returns>
        /// <response code="200">Returns the list of orderItems.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderItemViewDto>), 200)]
        public ActionResult<IEnumerable<OrderItem>> GetAll()
        {
            var orderItemsDto = _mapper.Map<List<OrderItemViewDto>>(_orderItems);
            return Ok(_orderItems);
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
            var orderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

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
            {
                return BadRequest(ModelState);
            }

            var orderItem = _mapper.Map<OrderItem>(orderItemDto);

            _orderItems.Add(orderItem);

            var createdOrderItem = _mapper.Map<OrderItemViewDto>(orderItem);

            return StatusCode(201, createdOrderItem);
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
            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }

            _mapper.Map(orderItemDto, existingOrderItem);

            var updatedOrderItem = _mapper.Map<OrderItemViewDto>(existingOrderItem);

            return Ok(updatedOrderItem);
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
            var orderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }

            _orderItems.Remove(orderItem);

            return NoContent();
        }
    }
}

using BookStorage.WebApi.DTOs.OrderItem;
using Microsoft.AspNetCore.Mvc;
using BookStorage.Domain.Models;

namespace BookStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
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
            var orderItemsDto = _orderItems.Select(a => new OrderItemViewDto
            {
                Id = a.Id,
                OrderId = a.OrderId,
                BookId = a.BookId,
            });

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

            var orderItemDto = new OrderItemViewDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                BookId = orderItem.BookId,
            };

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

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = orderItemDto.OrderId,
                BookId = orderItemDto.BookId,
                Order = new Order(),
                Book = new Book()
            };

            _orderItems.Add(orderItem);

            var createdOrderItem = new OrderItemViewDto
            {
                Id = orderItem.Id,
                OrderId = orderItemDto.OrderId,
                BookId = orderItemDto.BookId,
            };

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

            existingOrderItem.OrderId = orderItemDto.OrderId;
            existingOrderItem.BookId = orderItemDto.BookId;

            var updatedOrderItem = new OrderItemViewDto
            {
                Id = existingOrderItem.OrderId,
                OrderId = existingOrderItem.OrderId,
            };

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

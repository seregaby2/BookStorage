using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStorage.Controllers
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
        [ProducesResponseType(typeof(IEnumerable<OrderItem>), 200)]
        public ActionResult<IEnumerable<OrderItem>> GetAll()
        {
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
        [ProducesResponseType(typeof(OrderItem), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderItem> GetById([FromRoute] Guid id)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);
        }

        /// <summary>
        /// Creates a new orderItem
        /// </summary>
        /// <param name="orderItem">The orderItem to create</param>
        /// <returns>The newly created orderItem</returns>
        /// <response code="201">Returns the newly created orderItem</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderItem), 201)]
        [ProducesResponseType(400)]
        public ActionResult<OrderItem> Create([FromBody] OrderItem orderItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _orderItems.Add(orderItem);
            return StatusCode(201, orderItem);
        }

        /// <summary>
        /// Updates an existing orderItem by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the orderItem to update</param>
        /// <param name="orderItem">The updated orderItem data</param>
        /// <returns>The updated orderItem</returns>
        /// <response code="200">Returns the updated orderItem</response>
        /// <response code="404">If the orderItem with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderItem), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderItem> Update([FromRoute] Guid id, [FromBody] OrderItem orderItem)
        {
            var existingOrderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (existingOrderItem == null)
            {
                return NotFound();
            }
            existingOrderItem.Id = orderItem.Id;
            return Ok(_orderItems);
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

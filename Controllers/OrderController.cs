using BookStorage.DTOs.Order;
using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        public static readonly List<Order> _orders = new List<Order>
        {
          new Order
          {
              Id = Guid.NewGuid(),
              TotalAmount  = 200,
              OrderDate = new DateTime(),
              CustomerId  = Guid.NewGuid(),
              Customer = new Customer(),
              OrderItems = new List<OrderItem>()
          },
          new Order
          {
              Id = Guid.NewGuid(),
              TotalAmount  = 500,
              OrderDate = new DateTime(),
              CustomerId  = Guid.NewGuid(),
              Customer = new Customer(),
              OrderItems = new List<OrderItem>()
          }
        };

        /// <summary>
        /// Retrieves all orders available in the system
        /// </summary>
        /// <returns>A list of orders</returns>
        /// <response code="200">Returns the list of orders.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderViewDto>), 200)]
        public ActionResult<IEnumerable<OrderViewDto>> GetAll()
        {
            var ordersDto = _orders.Select(a => new OrderViewDto
            {
                Id =a.Id,
                TotalAmount = a.TotalAmount,
                OrderDate = a.OrderDate,
                CustomerId = a.CustomerId,
            });
            
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
            var order = _orders.FirstOrDefault(a => a.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            var orderDto = new OrderViewDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
            };

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
        /// <param name="order">The order to create</param>
        /// <returns>The newly created order</returns>
        /// <response code="201">Returns the newly created order</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(OrderViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<OrderViewDto> Create([FromBody] CreateOrderDto orderDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = new Order
            {
                Id = Guid.NewGuid(),
                TotalAmount = orderDto.TotalAmount,
                OrderDate = orderDto.OrderDate,
                CustomerId = orderDto.CustomerId,
            };

            _orders.Add(order);

            var createdOrder = new OrderViewDto
            {
                Id = order.Id,
                TotalAmount = order.TotalAmount,
                OrderDate = order.OrderDate,
                CustomerId = order.CustomerId,
            };

            return StatusCode(201, createdOrder);
        }

        /// <summary>
        /// Updates an existing order by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the order to update</param>
        /// <param name="order">The updated order data</param>
        /// <returns>The updated order</returns>
        /// <response code="200">Returns the updated order</response>
        /// <response code="404">If the order with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<OrderViewDto> Update([FromRoute] Guid id, [FromBody] UpdateOrderDto order)
        {
            var existingOrder = _orders.FirstOrDefault(x => x.Id == id);

            if (existingOrder == null)
            {
                return NotFound();
            }

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;

            var updatedDto = new OrderViewDto
            {
                Id = existingOrder.Id,
                TotalAmount = existingOrder.TotalAmount,
                OrderDate = existingOrder.OrderDate,
            };
            
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
            var order = _orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _orders.Remove(order);
            
            return NoContent();
        }
    }
}


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

        [HttpGet]
        public ActionResult<IEnumerable<OrderItem>> GetAll()
        {
            return Ok(_orderItems);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderItem> GetById([FromRoute] Guid id)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            return Ok(orderItem);

        }

        [HttpPost]
        public ActionResult<OrderItem> Create([FromBody] OrderItem item)
        {
            _orderItems.Add(item);
            return Ok(_orderItems);
        }

        [HttpPut("{id}")]
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

        [HttpDelete("{id}")]
        public ActionResult<OrderItem> Delete([FromRoute] Guid id)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.Id == id);
            if (orderItem == null)
            {
                return NotFound();
            }
            _orderItems.Remove(orderItem);
            return Ok(orderItem);
        }
    }
}

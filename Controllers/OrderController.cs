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
          },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Order>> GetAll()
        {
            return Ok(_orders);
        }

        [HttpGet("{id}")]
        public ActionResult<Order> GetById([FromRoute] Guid id)
        {
            var order = _orders.FirstOrDefault(x => x.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        [HttpPost]
        public ActionResult<Order> Create([FromBody] Order order)
        {
            _orders.Add(order);
            return Ok(order);              
        }

        [HttpPut("{id}")]
        public ActionResult<Order> Update([FromRoute] Guid id, [FromBody] Order order)
        {
            var existingOrder = _orders.FirstOrDefault(x => x.Id == id);
            if(existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.OrderDate = order.OrderDate;
            existingOrder.TotalAmount = order.TotalAmount;
            return Ok(existingOrder);
        }

        [HttpDelete("{id}")]
        public ActionResult<Order> Delete([FromRoute] Guid id)
        {
            var order = _orders.FirstOrDefault(x=>x.Id == id);
            if(order == null)
            {
                return NotFound();
            }
            _orders.Remove(order);
            return Ok(order);
        }
      };
}


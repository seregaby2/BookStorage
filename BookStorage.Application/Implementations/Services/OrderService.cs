using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class OrderService : IOrderService
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
        public IEnumerable<Order> GetAll()
        {
            return _orders;
        }

        public Order? GetById(Guid id)
        {
            var order = _orders.FirstOrDefault(a => a.Id == id);
            if (order == null)
                return null;

            return order;
        }

        public Order Create(Order order)
        {
            order.Id = Guid.NewGuid();

            _orders.Add(order);

            return order;
        }

        public Order? Update(Guid id, Order order)
        {
            var existingOrder = _orders.FirstOrDefault(a => a.Id == id);
            if (existingOrder == null)
                return null;

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.TotalAmount = order.TotalAmount;

            return existingOrder;
        }

        public Order Delete(Guid id)
        {
            var orderToDelete = _orders.FirstOrDefault(a => a.Id == id);
            if (orderToDelete == null)
                return null;

            _orders.Remove(orderToDelete);

            return orderToDelete;
        }
    }
}

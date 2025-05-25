using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class OrderItemService : IOrderItemService
    {
        public static readonly List<OrderItem> _ordersItem = new List<OrderItem>
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
        public IEnumerable<OrderItem> GetAll()
        {
            return _ordersItem;
        }

        public OrderItem? GetById(Guid id)
        {
            var orderItem = _ordersItem.FirstOrDefault(a => a.Id == id);
            if (orderItem == null)
                return null;

            return orderItem;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            orderItem.Id = Guid.NewGuid();

            _ordersItem.Add(orderItem);

            return orderItem;
        }

        public OrderItem? Update(Guid id, OrderItem orderItem)
        {
            var existingOrderItem = _ordersItem.FirstOrDefault(a => a.Id == id);
            if (existingOrderItem == null)
                return null;

            existingOrderItem.OrderId = Guid.NewGuid();
            existingOrderItem.BookId = Guid.NewGuid();

            return existingOrderItem;
        }

        public OrderItem? Delete(Guid id)
        {
            var orderToDelete = _ordersItem.FirstOrDefault(a => a.Id == id);
            if (orderToDelete == null)
                return null;

            _ordersItem.Remove(orderToDelete);

            return orderToDelete;
        }
    }
}

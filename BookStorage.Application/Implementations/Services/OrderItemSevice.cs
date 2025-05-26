using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class OrderItemService : IOrderItemService
    {
        private static readonly List<OrderItem> OrderItems =
        [
            new()
            {
                Id = Guid.NewGuid(),
                OrderId  = Guid.NewGuid(),
                Order = new Order(),
                BookId = Guid.NewGuid(),
                Book = new Book()
            },
            new()
            {
                Id = Guid.NewGuid(),
                OrderId  = Guid.NewGuid(),
                Order = new Order(),
                BookId = Guid.NewGuid(),
                Book = new Book()
            }
        ];
        public IEnumerable<OrderItem> GetAll()
        {
            return OrderItems;
        }

        public OrderItem? GetById(Guid id)
        {
            var orderItem = OrderItems.FirstOrDefault(a => a.Id == id);

            return orderItem;
        }

        public OrderItem Create(OrderItem orderItem)
        {
            orderItem.Id = Guid.NewGuid();

            OrderItems.Add(orderItem);

            return orderItem;
        }

        public OrderItem? Update(Guid id, OrderItem orderItem)
        {
            var existingOrderItem = OrderItems.FirstOrDefault(a => a.Id == id);
            if (existingOrderItem == null)
                return null;

            existingOrderItem.OrderId = Guid.NewGuid();
            existingOrderItem.BookId = Guid.NewGuid();

            return existingOrderItem;
        }

        public OrderItem? Delete(Guid id)
        {
            var orderToDelete = OrderItems.FirstOrDefault(a => a.Id == id);
            if (orderToDelete == null)
                return null;

            OrderItems.Remove(orderToDelete);

            return orderToDelete;
        }
    }
}

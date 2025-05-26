using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;

namespace BookStorage.Application.Implementations.Services
{
    public class OrderService : IOrderService
    {
        public static readonly List<Order> Orders =
        [
            new()
            {
                Id = Guid.NewGuid(),
                TotalAmount  = 200,
                OrderDate = new DateTime(),
                CustomerId  = Guid.NewGuid(),
                Customer = new Customer(),
                OrderItems = new List<OrderItem>()
            },
            new()
            {
                Id = Guid.NewGuid(),
                TotalAmount  = 500,
                OrderDate = new DateTime(),
                CustomerId  = Guid.NewGuid(),
                Customer = new Customer(),
                OrderItems = new List<OrderItem>()
            }
        ];
        public IEnumerable<Order> GetAll()
        {
            return Orders;
        }

        public Order? GetById(Guid id)
        {
            var order = Orders.FirstOrDefault(a => a.Id == id);

            return order;
        }

        public Order Create(Order order)
        {
            order.Id = Guid.NewGuid();

            Orders.Add(order);

            return order;
        }

        public Order? Update(Guid id, Order order)
        {
            var existingOrder = Orders.FirstOrDefault(a => a.Id == id);
            if (existingOrder == null)
                return null;

            existingOrder.OrderDate = order.OrderDate;
            existingOrder.CustomerId = order.CustomerId;
            existingOrder.TotalAmount = order.TotalAmount;

            return existingOrder;
        }

        public Order? Delete(Guid id)
        {
            var orderToDelete = Orders.FirstOrDefault(a => a.Id == id);
            if (orderToDelete == null)
                return null;

            Orders.Remove(orderToDelete);

            return orderToDelete;
        }
    }
}

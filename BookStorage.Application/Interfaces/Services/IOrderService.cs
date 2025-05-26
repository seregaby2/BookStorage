using BookStorage.Domain.Models;

namespace BookStorage.Application.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetAll();
        Order? GetById(Guid id);
        Order Create(Order order);
        Order? Update(Guid id, Order order);
        bool Delete(Guid id);
    }
}

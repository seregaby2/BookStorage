using BookStorage.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStorage.Application.Interfaces.Services
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItem> GetAll();
        OrderItem? GetById(Guid id);
        OrderItem Create(OrderItem orderItem);
        OrderItem? Update(Guid id, OrderItem orderItem);
        OrderItem? Delete(Guid id);
    }
}

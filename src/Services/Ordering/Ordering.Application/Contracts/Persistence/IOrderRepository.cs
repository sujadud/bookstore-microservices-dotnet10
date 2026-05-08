using Ordering.Domain.Entities;

namespace Ordering.Application.Contracts.Persistence;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
    Task<Order> AddOrder(Order order);
}

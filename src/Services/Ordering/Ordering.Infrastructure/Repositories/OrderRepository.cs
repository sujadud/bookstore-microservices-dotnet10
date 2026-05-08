using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    protected readonly OrderContext _dbContext;

    public OrderRepository(OrderContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
    {
        return await _dbContext.Orders
                            .Where(o => o.UserName == userName)
                            .ToListAsync();
    }

    public async Task<Order> AddOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }
}

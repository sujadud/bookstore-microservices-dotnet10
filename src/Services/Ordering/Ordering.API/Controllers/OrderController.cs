using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System.Net;

namespace Ordering.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class OrderController : BaseController
{
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrdersByUserName(string userName)
    {
        var orders = await _orderRepository.GetOrdersByUserName(userName);
        return CustomResult("Orders loaded successfully.", orders);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CheckoutOrder([FromBody] Order order)
    {
        var result = await _orderRepository.AddOrder(order);
        return CustomResult("Order checkout successfully.", result);
    }
}

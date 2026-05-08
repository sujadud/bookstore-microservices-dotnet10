using Basket.API.GrpcServices;
using Basket.API.Models;
using Basket.API.Repositories;
using CoreApiResponse;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class BasketController : BaseController
{
    private readonly IBasketRepository _repository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IPublishEndpoint _publishEndpoint;

    public BasketController(IBasketRepository repository, DiscountGrpcService discountGrpcService, IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _discountGrpcService = discountGrpcService;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}")]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetBasket(string userName)
    {
        var basket = await _repository.GetBasket(userName);
        return CustomResult("Basket loaded successfully.", basket ?? new ShoppingCart(userName));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
    {
        foreach (var item in basket.Items)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductId);
            item.Price -= coupon.Amount;
        }

        return CustomResult("Basket updated successfully.", await _repository.UpdateBasket(basket));
    }

    [HttpDelete("{userName}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        await _repository.DeleteBasket(userName);
        return CustomResult("Basket deleted successfully.", HttpStatusCode.OK);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.Accepted)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Checkout([FromBody] BasketCheckoutEvent basketCheckout)
    {
        var basket = await _repository.GetBasket(basketCheckout.UserName!);
        if (basket == null)
        {
            return BadRequest();
        }

        basketCheckout.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish(basketCheckout);

        await _repository.DeleteBasket(basket.UserName);

        return Accepted();
    }
}

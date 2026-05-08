using CoreApiResponse;
using Discount.API.Models;
using Discount.API.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DiscountController : BaseController
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetDiscount(string productId)
    {
        var discount = await _repository.GetDiscount(productId);
        return CustomResult("Discount loaded successfully.", discount);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
    {
        await _repository.CreateDiscount(coupon);
        return CustomResult("Discount created successfully.", coupon, HttpStatusCode.Created);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
    {
        await _repository.UpdateDiscount(coupon);
        return CustomResult("Discount updated successfully.", coupon);
    }

    [HttpDelete("{productId}")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> DeleteDiscount(string productId)
    {
        await _repository.DeleteDiscount(productId);
        return CustomResult("Discount deleted successfully.", HttpStatusCode.OK);
    }
}

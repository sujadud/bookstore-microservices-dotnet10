using AutoMapper;
using Discount.Grpc.Models;
using Discount.Grpc.Protos;
using Discount.Grpc.Repository;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly IDiscountRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<DiscountService> _logger;

    public DiscountService(IDiscountRepository repository, IMapper mapper, ILogger<DiscountService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _repository.GetDiscount(request.ProductId);
        if (coupon == null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.ProductId} is not found."));
        }
        _logger.LogInformation("Discount is retrieved for ProductId : {productId}, Amount : {amount}", coupon.ProductId, coupon.Amount);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        await _repository.CreateDiscount(coupon);
        _logger.LogInformation("Discount is successfully created. ProductId : {productId}", coupon.ProductId);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        await _repository.UpdateDiscount(coupon);
        _logger.LogInformation("Discount is successfully updated. ProductId : {productId}", coupon.ProductId);

        var couponModel = _mapper.Map<CouponModel>(coupon);
        return couponModel;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var deleted = await _repository.DeleteDiscount(request.ProductId);
        var response = new DeleteDiscountResponse
        {
            Success = deleted
        };

        return response;
    }
}

using Dapper;
using Discount.Grpc.Models;
using Npgsql;

namespace Discount.Grpc.Repository;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private NpgsqlConnection GetConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString("Discount.Grpc"));
    }

    public async Task<Coupon> GetDiscount(string productId)
    {
        using var connection = GetConnection();
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductId = @ProductId", new { ProductId = productId });

        if (coupon == null)
            return new Coupon { ProductId = "No Discount", Amount = 0, Description = "No Discount Desc" };

        return coupon;
    }

    public async Task<bool> CreateDiscount(Coupon coupon)
    {
        using var connection = GetConnection();
        var affected = await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductId, Description, Amount) VALUES (@ProductId, @Description, @Amount)",
            new { ProductId = coupon.ProductId, Description = coupon.Description, Amount = coupon.Amount });

        return affected != 0;
    }

    public async Task<bool> UpdateDiscount(Coupon coupon)
    {
        using var connection = GetConnection();
        var affected = await connection.ExecuteAsync
            ("UPDATE Coupon SET ProductId=@ProductId, Description=@Description, Amount=@Amount WHERE Id=@Id",
            new { ProductId = coupon.ProductId, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        return affected != 0;
    }

    public async Task<bool> DeleteDiscount(string productId)
    {
        using var connection = GetConnection();
        var affected = await connection.ExecuteAsync("DELETE FROM Coupon WHERE ProductId = @ProductId",
            new { ProductId = productId });

        return affected != 0;
    }
}

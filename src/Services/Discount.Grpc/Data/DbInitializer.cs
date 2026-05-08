using Dapper;
using Npgsql;

namespace Discount.Grpc.Data;

public static class DbInitializer
{
    public static async Task InitializeDatabase(IConfiguration configuration)
    {
        using var connection = new NpgsqlConnection(configuration.GetConnectionString("Discount.Grpc"));

        await connection.ExecuteAsync("CREATE TABLE IF NOT EXISTS Coupon(Id SERIAL PRIMARY KEY, ProductId VARCHAR(24) NOT NULL, Description TEXT, Amount INT)");

        var exists = await connection.QueryFirstOrDefaultAsync<int>("SELECT COUNT(*) FROM Coupon");
        if (exists == 0)
        {
            await connection.ExecuteAsync("INSERT INTO Coupon(ProductId, Description, Amount) VALUES('602d2149e773f2a3990b47f5', 'IPhone Discount', 150)");
            await connection.ExecuteAsync("INSERT INTO Coupon(ProductId, Description, Amount) VALUES('602d2149e773f2a3990b47f6', 'Samsung Discount', 100)");
        }
    }
}

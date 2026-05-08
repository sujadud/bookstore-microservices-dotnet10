using MongoRepo.Context;

namespace Catalog.API.Context;

public class CatalogDbContext : ApplicationDbContext
{
    public CatalogDbContext(IConfiguration configuration)
        : base(configuration.GetConnectionString("Catalog.API") ?? "mongodb://localhost:27017",
               configuration.GetValue<string>("DatabaseName") ?? "BookstoreCatalogDb")
    {
    }
}

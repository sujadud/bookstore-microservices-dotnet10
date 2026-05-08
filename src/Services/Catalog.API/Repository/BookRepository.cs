using Catalog.API.Context;
using Catalog.API.Interfaces.Repository;
using Catalog.API.Models;
using MongoRepo.Repository;

namespace Catalog.API.Repository;

public class BookRepository : CommonRepository<Book>, IBookRepository
{
    public BookRepository(CatalogDbContext dbContext) : base(dbContext)
    {
    }
}

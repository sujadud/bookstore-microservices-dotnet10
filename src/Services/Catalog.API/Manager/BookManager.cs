using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using Catalog.API.Repository;
using MongoRepo.Manager;

namespace Catalog.API.Manager;

public class BookManager : CommonManager<Book>, IBookManager
{
    public BookManager(BookRepository bookRepository) : base(bookRepository)
    {
    }
}

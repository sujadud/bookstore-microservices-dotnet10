using Catalog.API.Interfaces.Manager;
using Catalog.API.Models;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CatalogController : BaseController
{
    private readonly IBookManager _bookManager;

    public CatalogController(IBookManager bookManager)
    {
        _bookManager = bookManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public IActionResult GetBooks()
    {
        try
        {
            var books = _bookManager.GetAll();
            return CustomResult("Books loaded successfully.", books);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpGet("{category}")]
    [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
    public IActionResult GetByCategory(string category)
    {
        try
        {
            var books = _bookManager.GetAll(x => x.Category == category);
            return CustomResult("Books loaded successfully.", books);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public IActionResult GetById(string id)
    {
        try
        {
            var book = _bookManager.GetById(id);
            return CustomResult("Book loaded successfully.", book);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.Created)]
    public IActionResult CreateBook([FromBody] Book book)
    {
        try
        {
            book.Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            bool isSaved = _bookManager.Add(book);
            if (isSaved)
            {
                return CustomResult("Book saved successfully.", book, HttpStatusCode.Created);
            }
            return CustomResult("Book save failed.", HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
    public IActionResult UpdateBook([FromBody] Book book)
    {
        try
        {
            if (string.IsNullOrEmpty(book.Id))
            {
                return CustomResult("Book ID is required.", HttpStatusCode.NotFound);
            }
            bool isUpdated = _bookManager.Update(book.Id, book);
            if (isUpdated)
            {
                return CustomResult("Book updated successfully.", book, HttpStatusCode.OK);
            }
            return CustomResult("Book update failed.", HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult DeleteBook(string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return CustomResult("Book ID is required.", HttpStatusCode.NotFound);
            }
            bool isDeleted = _bookManager.Delete(id);
            if (isDeleted)
            {
                return CustomResult("Book deleted successfully.", HttpStatusCode.OK);
            }
            return CustomResult("Book deletion failed.", HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            return CustomResult(ex.Message, HttpStatusCode.BadRequest);
        }
    }
}

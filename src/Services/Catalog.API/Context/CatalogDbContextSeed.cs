using System;

namespace Catalog.API.Context;

public class CatalogDbContextSeed
{
    // public static void SeedData(IMongoCollection<Book> bookCollection) { bool existBooks = bookCollection.Find(b => true).Any(); if (!existBooks) { bookCollection.InsertManyAsync(GetPreconfiguredBooks()); } } private static IEnumerable<Book> GetPreconfiguredBooks() { return new List<Book> { new Book { Title = "Clean Code", Category = "Programming", Author = new[] { "Robert C. Martin" }, Description = "A Handbook of Agile Software Craftsmanship", CoverImageUrl = "https://example.com/cleancode.jpg", Price = 45.99M }, new Book { Title = "Domain-Driven Design", Category = "Architecture", Author = new[] { "Eric Evans" }, Description = "Tackling Complexity in the Heart of Software", CoverImageUrl = "https://example.com/ddd.jpg", Price = 59.99M } }; }
}

using MicroServicesEshopping.Caching;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServicesEshopping.Services
{
    public class ProductsRepo : IProductsRepo
    {
        private readonly IMongoCollection<Book> _booksCollection;
        private readonly ICacheService _cacheService;

        public ProductsRepo(IMongoDatabase database, ICacheService cacheService)
        {
            _booksCollection = database.GetCollection<Book>("Books");
            _cacheService = cacheService;
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var cacheKey = $"Book:{id}";
            var cachedBook = await _cacheService.GetData<Book>(cacheKey);

            if (cachedBook != null)
            {
                return cachedBook;
            }

            var book = await _booksCollection.Find(b => b.BooksId == id).FirstOrDefaultAsync();

            if (book != null)
            {
                await _cacheService.SetData(cacheKey, book, TimeSpan.FromMinutes(2));
            }

            return book;
        }

        public async Task<IList<Book>> GetBookByTitleAsync(string title)
        {
            var cacheKey = $"BooksByTitle:{title}";
            var cachedBooks = await _cacheService.GetData<IList<Book>>(cacheKey);

            if (cachedBooks != null)
            {
                return cachedBooks;
            }

            var filter = Builders<Book>.Filter.Regex(b => b.Name, new MongoDB.Bson.BsonRegularExpression(title, "i"));
            var books = await _booksCollection.Find(filter).ToListAsync();

            if (books.Count > 0)
            {
                await _cacheService.SetData(cacheKey, books, TimeSpan.FromMinutes(2));
            }

            return books;
        }

        public async Task<IList<Book>> GetBooksByAuthorAsync(string author)
        {
            var cacheKey = $"BooksByAuthor:{author}";
            var cachedBooks = await _cacheService.GetData<IList<Book>>(cacheKey);

            if (cachedBooks != null)
            {
                return cachedBooks;
            }

            var filter = Builders<Book>.Filter.Regex(b => b.Author, new MongoDB.Bson.BsonRegularExpression(author, "i"));
            var books = await _booksCollection.Find(filter).ToListAsync();

            if (books.Count > 0)
            {
                await _cacheService.SetData(cacheKey, books, TimeSpan.FromMinutes(2));
            }

            return books;
        }

        public async Task<IList<Book>> SearchBooksAsync(string searchTerm)
        {
            var cacheKey = $"BooksSearch:{searchTerm}";
            var cachedBooks = await _cacheService.GetData<IList<Book>>(cacheKey);

            if (cachedBooks != null)
            {
                return cachedBooks;
            }

            var filterTitle = Builders<Book>.Filter.Regex(b => b.Name, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            var filterAuthor = Builders<Book>.Filter.Regex(b => b.Author, new MongoDB.Bson.BsonRegularExpression(searchTerm, "i"));
            var filter = Builders<Book>.Filter.Or(filterTitle, filterAuthor);
            var books = await _booksCollection.Find(filter).ToListAsync();

            if (books.Count > 0)
            {
                await _cacheService.SetData(cacheKey, books, TimeSpan.FromMinutes(2));
            }

            return books;
        }

        public async Task<IList<Book>> GetAllBooksAsync()
        {
            var cacheKey = "AllBooks";
            var cachedBooks = await _cacheService.GetData<IList<Book>>(cacheKey);

            if (cachedBooks != null)
            {
                return cachedBooks;
            }

            var books = await _booksCollection.Find(_ => true).ToListAsync();

            if (books.Count > 0)
            {
                await _cacheService.SetData(cacheKey, books, TimeSpan.FromMinutes(2));
            }

            return books;
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            await _booksCollection.InsertOneAsync(book);
            await _cacheService.SetData($"Book:{book.BooksId}", book, TimeSpan.FromMinutes(2));
            await InvalidateCacheForAllBooks();
            return book;
        }

        public async Task<bool> UpdateBookAsync(Book book)
        {
            var result = await _booksCollection.ReplaceOneAsync(b => b.Id == book.Id, book);
            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                await _cacheService.SetData($"Book:{book.BooksId}", book, TimeSpan.FromMinutes(2));
                await InvalidateCacheForAllBooks();
            }
            return result.IsAcknowledged && result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var result = await _booksCollection.DeleteOneAsync(book => book.BooksId == id);
            if (result.IsAcknowledged && result.DeletedCount > 0)
            {
                await _cacheService.RemoveData($"Book:{id}");
                await InvalidateCacheForAllBooks();
            }
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        private async Task InvalidateCacheForAllBooks()
        {
            await _cacheService.RemoveData("AllBooks");
        }
    }
}

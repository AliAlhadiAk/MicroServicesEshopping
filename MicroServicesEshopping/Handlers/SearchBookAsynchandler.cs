using MediatR;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;
using Microsoft.Extensions.Logging;

namespace MicroServicesEshopping.Handlers
{
    public class SearchBookAsyncHandler : IRequestHandler<SearchBookAsyncQuery, IEnumerable<Book>>
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ILogger<SearchBookAsyncHandler> _logger;

        public SearchBookAsyncHandler(IProductsRepo productsRepo, ILogger<SearchBookAsyncHandler> logger)
        {
            _productsRepo = productsRepo;
            _logger = logger;
        }

        public async Task<IEnumerable<Book>> Handle(SearchBookAsyncQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.SearchTerm))
            {
                _logger.LogInformation("Search term is empty.");
                return Enumerable.Empty<Book>();
            }

            var books = await _productsRepo.SearchBooksAsync(request.SearchTerm);

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found for search term: {SearchTerm}", request.SearchTerm);
            }
            else
            {
                _logger.LogInformation("Found {Count} books for search term: {SearchTerm}", books.Count(), request.SearchTerm);
            }

            return books;
        }
    }
}

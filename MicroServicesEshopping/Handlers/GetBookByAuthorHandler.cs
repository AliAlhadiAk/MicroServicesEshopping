using MediatR;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;

namespace MicroServicesEshopping.Handlers
{
    public class GetBookByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, IList<Book>>
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ILogger<GetBookByAuthorHandler> _logger;

        public GetBookByAuthorHandler(IProductsRepo productsRepo, ILogger<GetBookByAuthorHandler> logger)
        {
            _productsRepo = productsRepo;
            _logger = logger;
        }

        public async Task<IList<Book>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Author))
            {
                _logger.LogInformation("Author parameter is null or empty.");
                return new List<Book>();
            }

            var books = await _productsRepo.GetBooksByAuthorAsync(request.Author);

            if (books == null || !books.Any())
            {
                _logger.LogInformation($"No books found for author: {request.Author}");
            }
            else
            {
                _logger.LogInformation($"Found {books.Count} books for author: {request.Author}");
            }

            return books;
        }
    }
}

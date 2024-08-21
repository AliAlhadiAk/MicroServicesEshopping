using MediatR;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;
using Microsoft.Extensions.Logging;

namespace MicroServicesEshopping.Handlers
{
    public class GetBooksByTitleHandler : IRequestHandler<GetBooksByTitleQuery, IList<Book>>
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ILogger<GetBooksByTitleHandler> _logger;

        public GetBooksByTitleHandler(IProductsRepo productsRepo, ILogger<GetBooksByTitleHandler> logger)
        {
            _productsRepo = productsRepo;
            _logger = logger;
        }

        public async Task<IList<Book>> Handle(GetBooksByTitleQuery request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Title))
            {
                _logger.LogInformation("Title query parameter is empty.");
                return new List<Book>();
            }

            var books = await _productsRepo.GetBookByTitleAsync(request.Title);

            if (books == null || !books.Any())
            {
                _logger.LogInformation("No books found with title: {Title}", request.Title);
            }
            else
            {
                _logger.LogInformation("Retrieved {Count} books with title: {Title}", books.Count, request.Title);
            }

            return books;
        }
    }
}

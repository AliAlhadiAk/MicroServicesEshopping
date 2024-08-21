using MediatR;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;
using Microsoft.Extensions.Logging;

namespace MicroServicesEshopping.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ILogger<GetBookByIdHandler> _logger;

        public GetBookByIdHandler(IProductsRepo productsRepo, ILogger<GetBookByIdHandler> logger)
        {
            _productsRepo = productsRepo;
            _logger = logger;
        }

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id < 1)
            {
                _logger.LogInformation("Invalid book ID: {BookId}", request.Id);
                return null;
            }

            var book = await _productsRepo.GetBookByIdAsync(request.Id);

            if (book == null)
            {
                _logger.LogInformation("No book found with ID: {BookId}", request.Id);
            }
            else
            {
                _logger.LogInformation("Retrieved book with ID: {BookId}", request.Id);
            }

            return book;
        }
    }
}

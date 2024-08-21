using MediatR;
using MicroServicesEshopping.Commands;
using MicroServicesEshopping.Services;

namespace MicroServicesEshopping.Handlers
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommmand, bool>
    {
        private readonly IProductsRepo _productRepo;
        private readonly ILogger<UpdateBookHandler> _logger;
        public UpdateBookHandler(IProductsRepo productsRepo, ILogger<UpdateBookHandler> logger)
        {
            _productRepo = productsRepo;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateBookCommmand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return false;
            }
            await _productRepo.UpdateBookAsync(request.book);
            _logger.LogInformation("Product Updated Succefully");
            return true;
        }
    }
}

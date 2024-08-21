using MediatR;
using MicroServicesEshopping.Commands;
using MicroServicesEshopping.Services;
using System.Runtime.CompilerServices;

namespace MicroServicesEshopping.Handlers
{
    public class DeleteBookByIdHandler : IRequestHandler<DeleteBookByIdCommnad, bool>
    {
        private readonly IProductsRepo _productRepo;
        private readonly ILogger<DeleteBookByIdHandler> _logger;
        public DeleteBookByIdHandler(IProductsRepo productsRepo, ILogger<DeleteBookByIdHandler> logger)
        {
            _productRepo = productsRepo;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteBookByIdCommnad request, CancellationToken cancellationToken)
        {
            if(request.Id == null || request.Id < 1)
            {
                return false;
            }
            var deleteBook = await _productRepo.DeleteBookAsync(request.Id);
            _logger.LogInformation("Book deleted Succefully");
            return true;
        }
    }
}

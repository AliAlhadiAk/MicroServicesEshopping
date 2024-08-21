using MediatR;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;

namespace MicroServicesEshopping.Handlers
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, IList<Book>>
    {
        private readonly IProductsRepo _productRepo;

        public GetAllBooksHandler(IProductsRepo productsRepo)
        {
            _productRepo = productsRepo;    
        }
        public async Task<IList<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _productRepo.GetAllBooksAsync();
            return books;
        }
    }
}

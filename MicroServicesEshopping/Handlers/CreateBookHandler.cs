using AutoMapper;
using MediatR;
using MicroServicesEshopping.Commands;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Services;

namespace MicroServicesEshopping.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, DTO_s.CreateBookDTO>
    {
        private readonly IProductsRepo _productsRepo;
        private readonly ILogger<CreateBookHandler> _logger;
        private readonly IMapper _mapper;
        public CreateBookHandler(IProductsRepo productsRepo, ILogger<CreateBookHandler> logger,IMapper mapper)
        {
            _productsRepo = productsRepo;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<DTO_s.CreateBookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if(request.Book == null)
            {
                _logger.LogInformation("Model is empty");
                return null;
            }
            var createBook = await _productsRepo.CreateBookAsync(request.Book);
            _logger.LogInformation("Book Created Sucefully");
            return _mapper.Map<CreateBookDTO>(createBook);

        }
    }
}

using MediatR;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Queries
{
    public class GetAllBooksQuery : IRequest<IList<Book>>
    {
    }
}

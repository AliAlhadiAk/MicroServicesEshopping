using MediatR;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public int Id { get; set; }

        public GetBookByIdQuery(int BookId)
        {
            Id = BookId;
        }
    }
}
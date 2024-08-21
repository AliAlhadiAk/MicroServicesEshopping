using MediatR;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Queries
{
    public class GetBooksByAuthorQuery : IRequest<IList<Book>>
    {
        public string Author { get; set; }

        public GetBooksByAuthorQuery(string author)
        {
            Author = author;
        }
    }
}

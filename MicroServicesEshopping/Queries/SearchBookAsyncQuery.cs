using MediatR;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Queries
{
    public class SearchBookAsyncQuery : IRequest<IEnumerable<Book>>
    {
        public string SearchTerm { get; set; }
        public SearchBookAsyncQuery(string Books)
        {
            SearchTerm = Books;
        }
    }
}

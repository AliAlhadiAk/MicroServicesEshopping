using MediatR;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Queries
{
    public class GetBooksByTitleQuery : IRequest<IList<Book>>
    {
        public string Title { get; set; }

        public GetBooksByTitleQuery(string title)
        {
            Title = title;
        }
    }
}

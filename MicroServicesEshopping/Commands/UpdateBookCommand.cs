using MediatR;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Commands
{
    public class UpdateBookCommmand : IRequest<bool>
    {
        public Book book { get; set; }

        public UpdateBookCommmand(Book Book)
        {
            book = Book;
        }
    }
}

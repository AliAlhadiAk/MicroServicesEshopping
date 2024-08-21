using MediatR;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Commands
{
    public class CreateBookCommand : IRequest<DTO_s.CreateBookDTO>
    {
        public Book Book { get; set; }

        public CreateBookCommand(Book book)
        {
            Book = book;
        }
    }
}

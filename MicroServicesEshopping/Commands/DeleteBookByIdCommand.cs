using MediatR;

namespace MicroServicesEshopping.Commands
{
    public class DeleteBookByIdCommnad : IRequest<bool>
    {
        public int Id { get; set; }
        public DeleteBookByIdCommnad(int BookId)
        {
            Id = BookId;
        }
    }
}

using MicroServicesEshopping.Model;

namespace MicroServicesEshopping.Services
{
    public interface IProductsRepo
    {
        //Queries
        Task<Book> GetBookByIdAsync(int id);
        //Queries

        Task<IList<Book>> GetBookByTitleAsync(string title);
        //Queries

        Task<IList<Book>> GetBooksByAuthorAsync(string author);
        //Queries

        Task<IList<Book>> SearchBooksAsync(string searchTerm);
        //Queries

        Task<IList<Book>> GetAllBooksAsync();
        Task<Book> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(Book book);
        Task<bool> DeleteBookAsync(int id);
    }
}

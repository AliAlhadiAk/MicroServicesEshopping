using MediatR;
using MicroServicesEshopping.Commands;
using MicroServicesEshopping.DTO_s;
using MicroServicesEshopping.Model;
using MicroServicesEshopping.Queries;
using MicroServicesEshopping.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MicroServicesEshopping.Controllers
{
    [Route("products/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query  = new GetAllBooksQuery();
            var books = await _mediator.Send(query);
            return Ok(books);   
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid book ID.");
            }

            var query = new GetBookByIdQuery(id);
            var book = await _mediator.Send(query);

            if (book == null)
            {
                return NotFound($"No book found with ID: {id}");
            }

            return Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookCommand command)
        {
            if (command?.Book == null)
            {
                return BadRequest("Book data is required.");
            }

            var createdBook = await _mediator.Send(command);

            if (createdBook == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the book.");
            }

            return CreatedAtAction(nameof(Get), new { id = createdBook.BooksId }, createdBook);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBook(Book book)
        {
            var command = new UpdateBookCommmand(book);
            await _mediator.Send(command);
            return Ok(command);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1)
            {
                return BadRequest("Invalid book ID.");
            }

            var result = await _mediator.Send(new DeleteBookByIdCommnad(id));

            if (!result)
            {
                return NotFound("Book not found.");
            }

            return NoContent();
        }
    }
}

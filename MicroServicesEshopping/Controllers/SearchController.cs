using MediatR;
using MicroServicesEshopping.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroServicesEshopping.Controllers
{
    [Route("products/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SearchController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("by-author")]
        public async Task<IActionResult> GetBooksByAuthor([FromQuery] string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return BadRequest("Author parameter is required.");
            }

            var query = new GetBooksByAuthorQuery(author);
            var books = await _mediator.Send(query);

            if (books == null || books.Count == 0)
            {
                return NotFound($"No books found for author: {author}");
            }

            return Ok(books);
        }

        [HttpGet("search/by-title")]
        public async Task<IActionResult> GetBooksByTitle([FromQuery] string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return BadRequest("Title query parameter cannot be empty.");
            }

            var query = new GetBooksByTitleQuery(title);
            var books = await _mediator.Send(query);

            if (books == null || !books.Any())
            {
                return NotFound($"No books found with title: {title}");
            }

            return Ok(books);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchBooks([FromQuery] string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return BadRequest("Search term cannot be empty.");
            }

            var query = new SearchBookAsyncQuery(searchTerm);
            var books = await _mediator.Send(query);

            if (books == null || !books.Any())
            {
                return NotFound($"No books found for search term: {searchTerm}");
            }

            return Ok(books);
        }
    }
}

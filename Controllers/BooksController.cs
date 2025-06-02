using BookManagementAPI.DTOs;
using BookManagementAPI.Application.Books.Commands;
using BookManagementAPI.Application.Books.Handlers;
using BookManagementAPI.Features.Books.Commands;
using BookManagementAPI.Features.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] BookRequest request)
        {
            var command = new CreateBookCommand(request);
            var result = await _mediator.Send(command);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _mediator.Send(new GetBookByIdQuery(id));
            return book is null
                ? NotFound(new { message = $"Book with ID {id} not found." })
                : Ok(book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequest request)
        {
            var updated = await _mediator.Send(new UpdateBookCommand(id, request));
            return updated is null
                ? NotFound(new { message = $"Book with ID {id} not found." })
                : Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var success = await _mediator.Send(new DeleteBookCommand(id));
            return !success
                ? NotFound(new { message = $"Book with ID {id} not found." })
                : NoContent();
        }
    }
}

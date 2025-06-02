using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using MediatR;

namespace BookManagementAPI.Features.Books.Commands
{
    public class UpdateBookCommand : IRequest<BookResponse?>
    {
        public int Id { get; }
        public BookRequest Request { get; }

        public UpdateBookCommand(int id, BookRequest request)
        {
            Id = id;
            Request = request;
        }
    }

    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookResponse?>
    {
        private readonly ApplicationDbContext _context;

        public UpdateBookCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookResponse?> Handle(UpdateBookCommand command, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(new object[] { command.Id }, cancellationToken);
            if (book == null) return null;

            book.Title = command.Request.Title;
            book.Author = command.Request.Author;
            

            await _context.SaveChangesAsync(cancellationToken);

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                UserId = book.UserId,
                IsRead = book.IsRead
                
            };
        }
    }
}
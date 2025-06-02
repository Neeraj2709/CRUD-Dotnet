
using BookManagementAPI.Application.Books.Commands;
using BookManagementAPI.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Features.Books.Handlers
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly ApplicationDbContext _context;

        public DeleteBookCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FindAsync(new object[] { request.Id }, cancellationToken);

            if (book == null)
                return false;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
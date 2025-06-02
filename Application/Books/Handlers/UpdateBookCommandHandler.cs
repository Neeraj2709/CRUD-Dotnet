using BookManagementAPI.Models;
using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Features.Books.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookResponse?>
    {
        private readonly ApplicationDbContext _context;

        public UpdateBookCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookResponse?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (book == null) return null;

            // Update book properties from request
            book.Title = request.Request.Title;
            book.Author = request.Request.Author;
            book.IsRead = request.Request.IsRead;
            book.UserId = request.Request.UserId;

            await _context.SaveChangesAsync(cancellationToken);

            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                IsRead = book.IsRead,
                UserId = book.UserId
            };
        }
    }
}
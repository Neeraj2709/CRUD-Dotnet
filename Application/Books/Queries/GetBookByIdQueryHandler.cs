using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace BookManagementAPI.Features.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookResponse?>
    {
        private readonly ApplicationDbContext _context;

        public GetBookByIdQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<BookResponse?> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (book == null) return null;

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
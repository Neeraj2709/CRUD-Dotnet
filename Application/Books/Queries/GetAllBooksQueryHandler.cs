using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookManagementAPI.Features.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookResponse>>
    {
        private readonly ApplicationDbContext _context;

        public GetAllBooksQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<BookResponse>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _context.Books.ToListAsync(cancellationToken);

            var result = new List<BookResponse>();
            foreach (var book in books)
            {
                result.Add(new BookResponse
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    IsRead = book.IsRead,
                    UserId = book.UserId
                });
            }
            return result;
        }
    }
}
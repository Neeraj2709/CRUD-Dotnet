using BookManagementAPI.Application.Books.Commands;

namespace BookManagementAPI.Application.Books.Handlers;

using BookManagementAPI.Database;
using BookManagementAPI.DTOs;
using BookManagementAPI.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookResponse>
{
    private readonly ApplicationDbContext _context;

    public CreateBookCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<BookResponse> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.BookRequest.UserId, cancellationToken);
        if (user == null) throw new Exception("User not found");

        var book = new Book
        {
            Title = request.BookRequest.Title,
            Author = request.BookRequest.Author,
            IsRead = request.BookRequest.IsRead,
            UserId = user.Id
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

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

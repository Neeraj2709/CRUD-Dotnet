using BookManagementAPI.DTOs;

namespace BookManagementAPI.Application.Books.Commands;
using MediatR;

public class CreateBookCommand : IRequest<BookResponse>
{
    public BookRequest BookRequest { get; }

    public CreateBookCommand(BookRequest request)
    {
        BookRequest = request;
    }
}

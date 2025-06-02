using BookManagementAPI.DTOs;
using MediatR;

namespace BookManagementAPI.Features.Books.Queries
{
    public class GetBookByIdQuery : IRequest<BookResponse?>
    {
        public int Id { get; }

        public GetBookByIdQuery(int id)
        {
            Id = id;
        }
    }
}
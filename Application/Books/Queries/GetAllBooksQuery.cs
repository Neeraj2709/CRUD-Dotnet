using BookManagementAPI.DTOs;
using MediatR;
using System.Collections.Generic;

namespace BookManagementAPI.Features.Books.Queries
{
    public class GetAllBooksQuery : IRequest<List<BookResponse>> { }
}

using MediatR;

namespace BookManagementAPI.Application.Books.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteBookCommand(int id)
        {
            Id = id;
        }
    }
}

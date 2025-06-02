using MediatR;
using BookManagementAPI.DTOs;

namespace BookManagementAPI.Users.Commands
{
    public class UpdateUsernameCommand : IRequest<UserResponse?>
    {
        public int Id { get; }
        public string NewUsername { get; }

        public UpdateUsernameCommand(int id, string newUsername)
        {
            Id = id;
            NewUsername = newUsername;
        }
    }
}